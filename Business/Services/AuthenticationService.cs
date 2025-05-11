using Business.Interfaces;
using DataAccess.Interfaces;
using Models;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public NguoiDung Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
                return null;

            // Kiểm tra mật khẩu bằng BCrypt
            if (BCrypt.Net.BCrypt.Verify(password, user.MatKhau))
                //if (user.MatKhau == password)
                return user;

            return null;
        }
        public RegisterResult Register(string username, string password, string email)
        {
            // Kiểm tra xem username đã tồn tại chưa
            if (_userRepository.GetUserByUsername(username) != null)
            {
                return new RegisterResult
                {
                    Success = false,
                    Message = "Tên đăng nhập đã tồn tại."
                };
            }

            if (_userRepository.GetUserByEmail(email) != null)
            {
                return new RegisterResult
                {
                    Success = false,
                    Message = "Email đã được sử dụng."
                };
            }

            var user = new NguoiDung
            {
                TenDangNhap = username,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email
            };

            _userRepository.AddUser(user);

            return new RegisterResult
            {
                Success = true,
                NguoiDung = user
            };
        }
        public ForgotPasswordResult ForgotPassword(string email)
        {
            // Tìm người dùng theo email
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = "Email không tồn tại."
                };
            }
            // Tạo mã xác nhận và gửi email
            string token = Guid.NewGuid().ToString();
            DateTime expiry = DateTime.UtcNow.AddMinutes(15); // Hết hạn sau 15 phút

            //  Lưu mã vào CSDL
            var maXacNhan = new MaXacNhanDatLaiMatKhau
            {
                NguoiDungId = user.NguoiDungId,
                MaXacNhan = token,
                ThoiGianHetHan = expiry,
                DaSuDung = false
            };
            _userRepository.LuuMaXacNhan(maXacNhan);

            // Gửi email
            try
            {
                string resetLink = $"https://localhost:44381/Account/ResetPassword?code={token}";
                SendPasswordResetEmail(user.Email, resetLink);
                return new ForgotPasswordResult
                {
                    Success = true,
                    Message = "Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư của bạn."
                };
            }
            catch (Exception ex)
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = "Lỗi khi gửi email: " + ex.Message
                };
            }
        }

        private void SendPasswordResetEmail(string toEmail, string resetLink)
        {
            var fromAddress = new MailAddress("2224802010514@student.tdmu.edu.vn", "Music Gawr");
            var toAddress = new MailAddress(toEmail);
            const string subject = "Yêu cầu đặt lại mật khẩu";
            string body = $@"<h2>Yêu cầu đặt lại mật khẩu</h2>
                           <p>Vui lòng nhấp vào liên kết sau để đặt lại mật khẩu của bạn:</p>
                           <p><a href='{resetLink}'>Đặt lại mật khẩu</a></p>
                           <p>Liên kết này sẽ hết hạn sau 15 phút.</p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                                    ConfigurationManager.AppSettings["SmtpEmail"],
                                    ConfigurationManager.AppSettings["SmtpAppPassword"])
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public ForgotPasswordResult ResetPassword(string token, string newPassword)
        {
            var resetToken = _userRepository.LayMaXacNhan(token);
            if (resetToken == null)
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = "Mã xác nhận không hợp lệ."
                };
            }

            if (resetToken.DaSuDung)
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = "Mã xác nhận đã được sử dụng."
                };
            }

            if (resetToken.ThoiGianHetHan < DateTime.UtcNow)
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = "Mã xác nhận đã hết hạn."
                };
            }

            var user = _userRepository.GetUserById(resetToken.NguoiDungId);
            // Cập nhật mật khẩu
            user.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.UpdateUserPassword(user);

            // Đánh dấu token đã sử dụng
            resetToken.DaSuDung = true;
            _userRepository.UpdateResetToken(resetToken);

            return new ForgotPasswordResult
            {
                Success = true,
                Message = "Mật khẩu đã được đặt lại thành công."
            };
        }
    }
}

using Business.Interfaces;
using Models;
using Models.DTOs;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public class RegisterService: IRegisterService
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public RegisterResultDto Register(RegisterViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password) || model.Password != model.ConfirmPassword)
                return new RegisterResultDto { Success = false, Message = "Thông tin không hợp lệ." };
            
            if (!IsStrongPassword(model.Password, out string passwordError))
            {
                return new RegisterResultDto
                {
                    Success = false,
                    Message = passwordError
                };
            }
            var result = _authenticationService.Register(model.Username, model.Password, model.Email);

            if(!result.Success)
            { return new RegisterResultDto { Success = false, Message = result.Message }; }    

            return new RegisterResultDto
            {
                Success = result.Success,
                Message = result.Message,
                User = new NguoiDungDto
                {
                    Id = result.NguoiDung.NguoiDungId,
                    Username = result.NguoiDung.TenDangNhap,
                    Email = result.NguoiDung.Email
                }
            };
        }
        private bool IsStrongPassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                errorMessage = "Mật khẩu phải có ít nhất 8 ký tự.";
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ in hoa.";
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ thường.";
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một số.";
                return false;
            }
            if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".Contains(ch)))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt.";
                return false;
            }

            return true;
        }

    }
}

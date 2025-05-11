using Business.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public class ResetPassword: IResetPassword
    {
        private readonly IAuthenticationService _authenticationService;

        public ResetPassword(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public ForgotPasswordResult ForgotPass(string email)
        {
            return _authenticationService.ForgotPassword(email);
        }

        public ForgotPasswordResult ResetPass(string token, string newPassword)
        {
            if (!IsStrongPassword(newPassword, out string errorMessage))
            {
                return new ForgotPasswordResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }

            return _authenticationService.ResetPassword(token, newPassword);
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

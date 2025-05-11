using Business.Interfaces;
using Models.DTOs;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public NguoiDungDto Login(LoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;

            var user = _authenticationService.Authenticate(model.Username, model.Password);
            if (user == null)
                return null;

            return new NguoiDungDto
            {
                Id = user.NguoiDungId,
                Username = user.TenDangNhap,
                Email = user.Email
            };
        }
    }
}

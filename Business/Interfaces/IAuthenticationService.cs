using Models;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        NguoiDung Authenticate(string username, string password);
        RegisterResult Register(string username, string password, string email);
        ForgotPasswordResult ForgotPassword(string email);
        ForgotPasswordResult ResetPassword(string token, string newPassword);
    }
}

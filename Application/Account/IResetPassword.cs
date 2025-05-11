using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public interface IResetPassword
    {
        ForgotPasswordResult ForgotPass(string email);
        ForgotPasswordResult ResetPass(string token, string newPassword);
    }
}

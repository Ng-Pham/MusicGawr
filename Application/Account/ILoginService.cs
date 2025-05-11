using Models.DTOs;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
    public interface ILoginService
    {
        NguoiDungDto Login(LoginViewModel model);
    }
}

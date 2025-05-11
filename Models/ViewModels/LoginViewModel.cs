using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

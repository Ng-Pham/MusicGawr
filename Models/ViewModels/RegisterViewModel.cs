using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống!")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp!")]
        public string ConfirmPassword { get; set; }
    }
}

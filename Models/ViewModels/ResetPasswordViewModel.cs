using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ResetPasswordViewModel : IValidatableObject
    {
        // Dùng cho Forgot
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        // Dùng cho Reset
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        // Dùng để xác định action đang gọi (Forgot hay Reset)
        public string Context { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Context == "Forgot")
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    yield return new ValidationResult("Email không được để trống!", new[] { nameof(Email) });
                }
            }
            else if (Context == "Reset")
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    yield return new ValidationResult("Mật khẩu mới không được để trống!", new[] { nameof(Password) });
                }
                else if (Password.Length < 8)
                {
                    yield return new ValidationResult("Mật khẩu phải có ít nhất 8 ký tự!", new[] { nameof(Password) });
                }

                if (string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    yield return new ValidationResult("Xác nhận mật khẩu không được để trống!", new[] { nameof(ConfirmPassword) });
                }
                else if (ConfirmPassword != Password)
                {
                    yield return new ValidationResult("Xác nhận mật khẩu không khớp!", new[] { nameof(ConfirmPassword) });
                }

                if (string.IsNullOrWhiteSpace(Code))
                {
                    yield return new ValidationResult("Mã xác nhận không được để trống!", new[] { nameof(Code) });
                }
            }
        }
    }
}

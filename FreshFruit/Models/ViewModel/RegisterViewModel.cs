using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email {  get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự.")]
        public string? Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }
    }
}

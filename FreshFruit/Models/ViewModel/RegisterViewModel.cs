using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập Email")]
        public string? Email {  get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

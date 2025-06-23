using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshFruit.Models
{
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên voucher là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Mã voucher là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Mã không được vượt quá 50 ký tự.")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Giá trị giảm là bắt buộc.")]
        [Range(1, 10000000, ErrorMessage = "Giá trị giảm phải lớn hơn 0.")]
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        public DateTime EndDate { get; set; }

        public int Status { get; set; } = 1; // 1 = hoạt động, 0 = ngừng
    }
}

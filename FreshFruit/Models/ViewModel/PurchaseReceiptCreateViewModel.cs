using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class PurchaseReceiptCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp.")]
        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không vượt quá 100 ký tự.")]
        public string? Supplier { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày nhập.")]
        public DateOnly ReceiptDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [MinLength(1, ErrorMessage = "Phải có ít nhất một sản phẩm.")]
        public List<PurchaseItemVM> Items { get; set; } = new List<PurchaseItemVM>();
    }

    public class PurchaseItemVM
    {
        [Required(ErrorMessage = "Vui lòng chọn sản phẩm.")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn 0.")]
        public decimal ImportPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hạn sử dụng.")]
        public DateOnly ExpiryDate { get; set; }
    }
}

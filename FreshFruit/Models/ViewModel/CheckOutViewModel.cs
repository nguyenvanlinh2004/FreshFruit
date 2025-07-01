namespace FreshFruit.Models.ViewModel
{
    public class CheckOutViewModel
    {
        public Invoice invoice { get; set; } = new Invoice(); // không để null!

        public List<CartItem>? CartItems { get; set; }
        public decimal? TotalPrice { get; set; }

        // Dùng cho hiển thị thông tin
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? SelectedVoucherCode { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal FinalPrice => (TotalPrice ?? 0) - DiscountAmount;
        public List<Voucher>? Vouchers { get; set; }      
    }
}

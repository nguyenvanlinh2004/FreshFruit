namespace FreshFruit.Models.ViewModel
{
    public class CheckOutViewModel
    {
        public Invoice invoice { get; set; } = new Invoice(); // không để null!

        public List<CartItem>? CartItems { get; set; }
        public decimal? TotalPrice { get; set; }

        // Dùng cho hiển thị thông tin
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}

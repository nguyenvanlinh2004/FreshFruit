namespace FreshFruit.Models.ViewModel
{
    public class CheckOutViewModel
    {
        public Invoice invoice { get; set; } = null!;
        public List<CartItem>? CartItems { get; set; } = null!;
        public decimal? TotalPrice { get; set; }
    }
}

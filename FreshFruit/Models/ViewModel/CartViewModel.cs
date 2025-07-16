namespace FreshFruit.Models.ViewModel
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }
        public decimal TotalQuantity { get; set; }
        public int TotalItems { get; set; }
    }
}   

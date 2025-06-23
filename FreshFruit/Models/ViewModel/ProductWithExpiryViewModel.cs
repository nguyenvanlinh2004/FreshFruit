namespace FreshFruit.Models.ViewModel
{
    public class ProductWithExpiryViewModel
    {
        public Product? Product { get; set; }
        public List<PurchaseReceiptDetail> ExpiringLots { get; set; } = new();

    }
}

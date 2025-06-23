namespace FreshFruit.Models.ViewModel
{
    public class ProductLotDetailVM
    {
        public Product? Product { get; set; }
        public List<LotViewModel> Lots { get; set; } = new();
    }

    public class LotViewModel
    {
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal Total { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? ShipmentId { get; set; }
    }

}

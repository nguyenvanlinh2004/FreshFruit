namespace FreshFruit.Models.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

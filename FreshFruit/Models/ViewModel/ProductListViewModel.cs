namespace FreshFruit.Models.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
		public string? SearchName { get; set; }
		public List<int>? SelectedCategoryIds { get; set; }
		public double? PriceFrom { get; set; }
		public double? PriceTo { get; set; }
	}
}

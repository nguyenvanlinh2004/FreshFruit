namespace FreshFruit.Models.ViewModel
{
	public class ProductViewModel
	{
		public required Product Product { get; set; }
		public double AverageRating { get; set; }
		public int TotalSold { get; set; }
	}
}

using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreshFruit.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly FreshFruitDbContext _context;

		public HomeController(ILogger<HomeController> logger, FreshFruitDbContext context)
		{
			_logger = logger;
			_context = context;
		}


		public required Product Product { get; set; }

		public async Task<IActionResult> Index()
		{
			// Lấy toàn bộ sản phẩm
			var allProducts = await _context.Products.ToListAsync();

			// Top 3 sản phẩm được đánh giá nhiều nhất
			var topRated = await _context.Products
				.Include(p => p.Ratings)
				.Include(p => p.InvoiceDetails)
				.OrderByDescending(p => p.Ratings.Count)
				.Take(3)
				.Select(p => new ProductViewModel
				{
					Product = p,
					AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0,
					TotalSold = p.InvoiceDetails.Any() ? (int)p.InvoiceDetails.Sum(i => i.Quantity) : 0
				})
				.ToListAsync();

			// Top 3 sản phẩm bán chạy nhất
			var topSelling = await _context.InvoiceDetails
				.GroupBy(i => i.ProductId)
				.Select(g => new
				{
					ProductId = g.Key,
					TotalSold = g.Sum(i => i.Quantity)
				})
				.OrderByDescending(x => x.TotalSold)
				.Take(3)
				.Join(
					_context.Products.Include(p => p.Ratings).Include(p => p.InvoiceDetails),
					sale => sale.ProductId,
					product => product.Id,
					(sale, product) => new ProductViewModel
					{
						Product = product,
						AverageRating = product.Ratings.Any() ? product.Ratings.Average(r => r.RatingValue) : 0,
						TotalSold = (int)sale.TotalSold
					})
				.ToListAsync();

			ViewBag.AllProducts = allProducts;
			ViewBag.TopRated = topRated;
			ViewBag.TopSelling = topSelling;

			return View();
		}

	}


}


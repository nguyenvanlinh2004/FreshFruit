using System.Diagnostics;
using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


		public async Task<IActionResult> Index()
		{
			var allProducts = await _context.Products.ToListAsync();

			var topRatedProducts = await _context.Products
				.Include(p => p.Ratings)
				.OrderByDescending(p => p.Ratings.Count)
				.Take(3)
				.ToListAsync();

			var topSellingProducts = await _context.InvoiceDetails
				.GroupBy(d => d.ProductId)
				.Select(g => new
				{
					ProductId = g.Key,
					TotalSold = g.Sum(x => x.Quantity)
				})
				.OrderByDescending(x => x.TotalSold)
				.Take(3)
				.Join(_context.Products,
					  sale => sale.ProductId,
					  product => product.Id,
					  (sale, product) => product)
				.ToListAsync();

			ViewBag.AllProducts = allProducts;
			ViewBag.TopRated = topRatedProducts;
			ViewBag.TopSelling = topSellingProducts;

			return View();
		}
	}

}


using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public ProductController(FreshFruitDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var products = _context.Products.Include(p => p.Category).ToList();
			return View(products);
		}
		public IActionResult AddProduct()
		{
			return View();
		}
		public IActionResult Edit(int id)
		{
			var product = _context.Products.Find(id);
			if (product == null) return NotFound();
			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			_context.Update(product);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var product = _context.Products.Find(id);
			if (product == null) return NotFound();

			_context.Products.Remove(product);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		
	}





}


using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class InvoiceController : Controller
	{
		private readonly FreshFruitDbContext _context;
		private const int PageSize = 10;

		public InvoiceController(FreshFruitDbContext context)
		{
			_context = context;
		}

		// Hàm bỏ dấu
		private string RemoveDiacritics(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;

			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in normalized)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}

			return sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
		}

		public async Task<IActionResult> Index(string searchCustomer, decimal? minTotal, decimal? maxTotal, int page = 1)
		{
			var allInvoices = await _context.Invoices
				.Include(i => i.Member)
				.OrderByDescending(i => i.InvoiceDate)
				.ToListAsync();

			// Lọc tên khách hàng
			if (!string.IsNullOrEmpty(searchCustomer))
			{
				var noD = RemoveDiacritics(searchCustomer);
				allInvoices = allInvoices.Where(i =>
					i.Member != null &&
					!string.IsNullOrEmpty(i.Member.Fullname) &&
					RemoveDiacritics(i.Member.Fullname).Contains(noD)).ToList();
			}

			if (minTotal.HasValue)
				allInvoices = allInvoices.Where(i => i.Total >= minTotal.Value).ToList();

			if (maxTotal.HasValue)
				allInvoices = allInvoices.Where(i => i.Total <= maxTotal.Value).ToList();

			var totalItems = allInvoices.Count;
			var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
			var pagedInvoices = allInvoices.Skip((page - 1) * PageSize).Take(PageSize).ToList();

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;
			ViewBag.SearchCustomer = searchCustomer;
			ViewBag.MinTotal = minTotal;
			ViewBag.MaxTotal = maxTotal;

			return View(pagedInvoices);
		}

		public async Task<IActionResult> ToggleStatus(int id)
		{
			var invoice = await _context.Invoices.FindAsync(id);
			if (invoice == null) return NotFound();

			invoice.Status = invoice.Status == 1 ? 0 : 1;
			_context.Update(invoice);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}

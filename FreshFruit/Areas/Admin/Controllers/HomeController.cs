using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public HomeController(FreshFruitDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Index(
	DateTime? startDate, DateTime? endDate,
	int? monthStart, int? monthEnd,
	int? quarterStart, int? quarterEnd,
	int? quarterYear,
	string? filterType // "day", "month", "quarter"
)
		{
			var adminId = HttpContext.Session.GetInt32("AdminId");
			if (adminId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var invoicesQuery = _context.Invoices.AsQueryable();

			List<Invoice> invoicesDay = new();
			List<Invoice> invoicesMonth = new();
			List<Invoice> invoicesQuarter = new();

			// Lọc theo ngày
			if (filterType == "day" && startDate.HasValue && endDate.HasValue)
			{
				var s = startDate.Value.Date;
				var e = endDate.Value.Date;

				invoicesDay = await invoicesQuery
					.Where(i => i.InvoiceDate.Date >= s && i.InvoiceDate.Date <= e)
					.ToListAsync();

				ViewBag.StartDate = s.ToString("yyyy-MM-dd");
				ViewBag.EndDate = e.ToString("yyyy-MM-dd");
			}
			else
			{
				ViewBag.StartDate = "";
				ViewBag.EndDate = "";
			}

			// Lọc theo tháng
			if (filterType == "month" && monthStart.HasValue && monthEnd.HasValue && quarterYear.HasValue)
			{
				invoicesMonth = await invoicesQuery
					.Where(i => i.InvoiceDate.Month >= monthStart &&
								i.InvoiceDate.Month <= monthEnd &&
								i.InvoiceDate.Year == quarterYear)
					.ToListAsync();

				ViewBag.MonthStart = monthStart.ToString();
				ViewBag.MonthEnd = monthEnd.ToString();
			}
			else
			{
				ViewBag.MonthStart = "";
				ViewBag.MonthEnd = "";
			}

			// Lọc theo quý
			if (filterType == "quarter" && quarterStart.HasValue && quarterEnd.HasValue && quarterYear.HasValue)
			{
				int startMonth = (quarterStart.Value - 1) * 3 + 1;
				int endMonth = (quarterEnd.Value - 1) * 3 + 3;

				invoicesQuarter = await invoicesQuery
					.Where(i => i.InvoiceDate.Month >= startMonth &&
								i.InvoiceDate.Month <= endMonth &&
								i.InvoiceDate.Year == quarterYear)
					.ToListAsync();

				ViewBag.QuarterStart = quarterStart.ToString();
				ViewBag.QuarterEnd = quarterEnd.ToString();
				ViewBag.QuarterYear = quarterYear.ToString();
			}
			else
			{
				ViewBag.QuarterStart = "";
				ViewBag.QuarterEnd = "";
				ViewBag.QuarterYear = "";
			}

			// Thống kê doanh thu theo ngày
			var dailyRevenue = invoicesDay
				.GroupBy(i => i.InvoiceDate.Date)
				.Select(g => new RevenueChartViewModel
				{
					Label = g.Key.ToString("yyyy-MM-dd"),
					TotalRevenue = g.Sum(i => i.Total)
				})
				.OrderBy(x => x.Label)
				.ToList();

			// Thống kê doanh thu theo tháng
			var monthlyRevenue = invoicesMonth
				.GroupBy(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month })
				.Select(g => new RevenueChartViewModel
				{
					Label = $"{g.Key.Year}-{g.Key.Month:D2}",
					TotalRevenue = g.Sum(i => i.Total)
				})
				.OrderBy(x => x.Label)
				.ToList();

			// Thống kê doanh thu theo quý
			var quarterlyRevenue = invoicesQuarter
				.GroupBy(i => new { i.InvoiceDate.Year, Quarter = (i.InvoiceDate.Month - 1) / 3 + 1 })
				.Select(g => new RevenueChartViewModel
				{
					Label = $"Q{g.Key.Quarter} - {g.Key.Year}",
					TotalRevenue = g.Sum(i => i.Total)
				})
				.OrderBy(x => x.Label)
				.ToList();

			// Top 3 sản phẩm bán chạy nhất
			var topProducts = await _context.InvoiceDetails
				.Include(d => d.Product)
				.GroupBy(d => new { d.ProductId, d.Product.Name })
				.Select(g => new ProductStatisticsViewModel
				{
					ProductName = g.Key.Name ?? "Unknown",
					TotalQuantitySold = g.Sum(d => d.Quantity)
				})
				.OrderByDescending(g => g.TotalQuantitySold)
				.Take(3)
				.ToListAsync();

			// Bottom 3 sản phẩm bán ít nhất
			var bottomProducts = await _context.InvoiceDetails
				.Include(d => d.Product)
				.GroupBy(d => new { d.ProductId, d.Product.Name })
				.Select(g => new ProductStatisticsViewModel
				{
					ProductName = g.Key.Name ?? "Unknown",
					TotalQuantitySold = g.Sum(d => d.Quantity)
				})
				.OrderBy(g => g.TotalQuantitySold)
				.Take(3)
				.ToListAsync();

			var viewModel = new ProductStatisticsPageViewModel
			{
				DailyRevenue = dailyRevenue,
				MonthlyRevenue = monthlyRevenue,
				QuarterlyRevenue = quarterlyRevenue,
				TopProducts = topProducts,
				BottomProducts = bottomProducts,
				TotalMembers = await _context.Members.CountAsync(),
				TotalPurchaseReceipts = await _context.PurchaseReceipts.CountAsync(),
				TotalInvoices = await _context.Invoices.CountAsync()
			};

			return View(viewModel);
		}

	}
}

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
		public async Task<IActionResult> Index()
		{
			// Doanh thu theo ngày
			var dailyRevenueRaw = await _context.Invoices
				.GroupBy(i => i.InvoiceDate)
				.Select(g => new
				{
					InvoiceDate = g.Key,
					TotalRevenue = g.Sum(e => e.Total)
				})
				.OrderBy(x => x.InvoiceDate)
				.ToListAsync();

			var dailyRevenue = dailyRevenueRaw
				.Select(x => new RevenueChartViewModel
				{
					Label = x.InvoiceDate.ToString("yyyy-MM-dd"),
					TotalRevenue = x.TotalRevenue
				})
				.ToList();

			// Doanh thu theo tháng
			var monthlyRevenueRaw = await _context.Invoices
				.GroupBy(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month })
				.Select(g => new
				{
					Year = g.Key.Year,
					Month = g.Key.Month,
					TotalRevenue = g.Sum(e => e.Total)
				})
				.OrderBy(x => x.Year).ThenBy(x => x.Month)
				.ToListAsync();

			var monthlyRevenue = monthlyRevenueRaw
				.Select(x => new RevenueChartViewModel
				{
					Label = $"{x.Year}-{x.Month:D2}",
					TotalRevenue = x.TotalRevenue
				})
				.ToList();

			// Doanh thu theo quý
			var quarterlyRevenueRaw = await _context.Invoices
				.GroupBy(i => new { i.InvoiceDate.Year, Quarter = (i.InvoiceDate.Month - 1) / 3 + 1 })
				.Select(g => new
				{
					Year = g.Key.Year,
					Quarter = g.Key.Quarter,
					TotalRevenue = g.Sum(e => e.Total)
				})
				.OrderBy(x => x.Year).ThenBy(x => x.Quarter)
				.ToListAsync();

			var quarterlyRevenue = quarterlyRevenueRaw
				.Select(x => new RevenueChartViewModel
				{
					Label = $"Q{x.Quarter} - {x.Year}",
					TotalRevenue = x.TotalRevenue
				})
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

			// Top 3 sản phẩm bán ít nhất
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

			// Tổng số thành viên
			var totalMembers = await _context.Members.CountAsync();

			// Tổng số phiếu nhập hàng
			var totalPurchaseReceipts = await _context.PurchaseReceipts.CountAsync();

			// Tổng số đơn hàng
			var totalInvoices = await _context.Invoices.CountAsync();

			// Gửi dữ liệu ra ViewModel
			var viewModel = new ProductStatisticsPageViewModel
			{
				TopProducts = topProducts,
				BottomProducts = bottomProducts,
				DailyRevenue = dailyRevenue,
				MonthlyRevenue = monthlyRevenue,
				QuarterlyRevenue = quarterlyRevenue,
				TotalMembers = totalMembers,
				TotalPurchaseReceipts = totalPurchaseReceipts,
				TotalInvoices = totalInvoices
			};

			return View(viewModel);
		}

	}
}

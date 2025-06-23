namespace FreshFruit.Models.ViewModel
{
	// ViewModel cho sản phẩm thống kê
	public class ProductStatisticsViewModel
	{
		public string ProductName { get; set; } = string.Empty;
		public decimal TotalQuantitySold { get; set; }
	}

	// ViewModel cho doanh thu thống kê (cho biểu đồ)
	public class RevenueChartViewModel
	{
		public string Label { get; set; } = string.Empty; // Ngày, Tháng, Quý
		public decimal TotalRevenue { get; set; }
	}

	// ViewModel tổng để truyền về view
	public class ProductStatisticsPageViewModel
	{
		// Top 3 sản phẩm bán chạy
		public List<ProductStatisticsViewModel> TopProducts { get; set; } = new();

		// Top 3 sản phẩm bán ít nhất
		public List<ProductStatisticsViewModel> BottomProducts { get; set; } = new();

		// Dữ liệu doanh thu theo ngày
		public List<RevenueChartViewModel> DailyRevenue { get; set; } = new();

		// Dữ liệu doanh thu theo tháng
		public List<RevenueChartViewModel> MonthlyRevenue { get; set; } = new();

		// Dữ liệu doanh thu theo quý
		public List<RevenueChartViewModel> QuarterlyRevenue { get; set; } = new();

		// Tổng số thành viên
		public int TotalMembers { get; set; }

		// Tổng số phiếu nhập hàng
		public int TotalPurchaseReceipts { get; set; }

		// Tổng số đơn hàng
		public int TotalInvoices { get; set; }
	}
}

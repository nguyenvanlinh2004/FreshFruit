using FreshFruit.Models;

namespace FreshFruit.Helpers
{
    public static class InvoiceHelper
    {
        /// <summary>
        /// Tạo mã hóa đơn tự động dạng HD20250620-ABC123
        /// </summary>
        public static string GenerateInvoiceCode()
        {
            var random = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"HD{DateTime.Now:yyyyMMdd}-{random}";
        }

        /// <summary>
        /// Tính tổng tiền từ danh sách chi tiết hóa đơn
        /// </summary>
        public static decimal CalculateInvoiceTotal(List<InvoiceDetail> details)
        {
            return details.Sum(d => d.UnitPrice * d.Quantity );
        }
    }
}

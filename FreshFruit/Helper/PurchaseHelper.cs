using FreshFruit.Models;

namespace FreshFruit.Helpers
{
    public static class PurchaseHelper
    {
        /// <summary>
        /// Tính tổng tiền tất cả các dòng chi tiết
        /// </summary>
        public static double CalculateTotal(List<PurchaseReceiptDetail> details)
        {
            return details.Sum(d => (double)(d.ImportPrice * d.Quantity ?? 0));
        }

        /// <summary>
        /// Cập nhật tồn kho cho các sản phẩm dựa trên các dòng chi tiết nhập
        /// </summary>
        public static void UpdateProductStock(FreshFruitDbContext context, List<PurchaseReceiptDetail> details, string? shipmentId = null)
        {
            foreach (var detail in details)
            {
                var product = context.Products.FirstOrDefault(p => p.Id == detail.ProductId);
                if (product != null)
                {
                    product.Quantity = (product.Quantity ?? 0) + (detail.Quantity ?? 0);

                    if (!string.IsNullOrEmpty(shipmentId))
                        product.ShipmentId = shipmentId;
                }
            }
        }

        /// <summary>
        /// Tạo mã lô hàng tự động (vd: LO20250620-ABC123)
        /// </summary>
        public static string GenerateShipmentCode()
        {
            var random = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            return $"LO{DateTime.Now:yyyyMMdd}-{random}";
        }
    }
}

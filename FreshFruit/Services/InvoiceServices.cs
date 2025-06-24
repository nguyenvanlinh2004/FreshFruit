using FreshFruit.Models;

namespace FreshFruit.Services
{
    public class InvoiceServices:IInvoiceServices
    {
        private readonly FreshFruitDbContext _context;

        public InvoiceServices(FreshFruitDbContext context)
        {
            _context = context;
        }
        public string GenerateOrderCode()
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            var maxOrderId = _context.Invoices.Max(o => (int?)o.Id);
            return $"ORD{currentDate}{maxOrderId + 1}";
        }
    }
}

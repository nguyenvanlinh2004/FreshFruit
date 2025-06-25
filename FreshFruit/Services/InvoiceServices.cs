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
            return $"HD{DateTime.Now:yyyyMMddHHmmssfff}";
        }
    }
}

using Microsoft.EntityFrameworkCore;
using FreshFruit.Models;
namespace FreshFruit.Services
{
    public class ProductServices:IProductServices
    {
        private readonly FreshFruitDbContext _context;

        public ProductServices(FreshFruitDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product!;
        }
    }
}

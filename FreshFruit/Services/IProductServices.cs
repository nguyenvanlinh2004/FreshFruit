using FreshFruit.Models;

namespace FreshFruit.Services
{
    public interface IProductServices
    {
        Task<ICollection<Product>> GetProducts();
        Task<Product> GetProduct(int productId);
    }
}

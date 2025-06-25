using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Controllers
{
    public class WishController : Controller
    {
        private readonly FreshFruitDbContext _context;

        public WishController(FreshFruitDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId == null)
                return RedirectToAction("Login", "Account");

            // ✅ Kiểm tra xem sản phẩm tồn tại không
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            var exists = _context.WishLists.Any(w => w.AccountID == accountId && w.ProductId == productId);
            if (!exists)
            {
                var wishlist = new WishList
                {
                    AccountID = (int)accountId,
                    ProductId = productId
                };
                _context.WishLists.Add(wishlist);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Wish");
        }


        public IActionResult Index()
        {
            var memberId = HttpContext.Session.GetInt32("AccountId");
            if (memberId == null)
                return RedirectToAction("Login", "Account");

            var wishlist = _context.WishLists
                .Include(w => w.Product)
                .Where(w => w.AccountID == memberId)
                .ToList();

            return View(wishlist);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var item = _context.WishLists.Find(id);
            if (item != null)
            {
                _context.WishLists.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Controllers
{
    public class ShopController : Controller
    {
        private readonly FreshFruitDbContext _context;

        public ShopController(FreshFruitDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var query = _context.Products
                        .Include(p => p.Category)
                        .Where(p => p.Status == 1);

            var totalProducts = _context.Products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(vm);
        }
        [Route("ProductDetail/{id}")]
        public IActionResult ProductDetail(int id)
        {
            var product = _context.Products
                          .Include(p => p.ProductImages)
                          .Include(p => p.Ratings)
                          .Include(p => p.Comments)
                          .FirstOrDefault(p => p.Id == id && p.Status == 1);

            if (product == null) return NotFound();

            var vm = new ProductDetailViewModel
            {
                Product = product,
                AverageRating = product.Ratings.Any() ? product.Ratings.Average(r => r.RatingValue) : 0,
                RelatedProducts = _context.Products
                                    .Where(p => p.CategoryId == product.CategoryId && p.Id != id && p.Status == 1)
                                    .Take(4).ToList()
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult SubmitRating(ProductDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Load lại dữ liệu nếu có lỗi
                model.Product = _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == model.ProductId);

                model.ProductImages = _context.ProductImages
                    .Where(pi => pi.ProductId == model.ProductId).ToList();

                model.RatingsWithComments = (from r in _context.Ratings
                                             join c in _context.Comments on r.Id equals c.Id into rc
                                             from Comment in rc.DefaultIfEmpty()
                                             where r.ProductId == model.ProductId
                                             select new RatingWithCommentVM
                                             {
                                                 RatingId = r.Id,
                                                 ProductId = r.ProductId,
                                                 MemberId = r.MemberId,
                                                 RatingValue = r.RatingValue,
                                                 CreatedAt = r.CreatedAt,
                                                 Status = r.Status,
                                                 CommentText = Comment != null ? Comment.Contents : null,
                                                 CommentCreatedAt = Comment != null ? Comment.CreatedAt : null,
                                                 CommentStatus = Comment != null ? Comment.Status : null
                                             }).ToList();

                model.RelatedProducts = _context.Products
                    .Where(p => p.CategoryId == model.Product!.CategoryId && p.Id != model.ProductId && p.Status == 1)
                    .Take(4).ToList();

                model.AverageRating = model.RatingsWithComments.Any()
                    ? model.RatingsWithComments.Average(r => r.RatingValue)
                    : 0;

                return View("ProductDetail", model);
            }

            // Giả lập lấy MemberId (hoặc lấy từ đăng nhập)
            var memberId = HttpContext.Session.GetInt32("MemberId") ?? 1;

            // Tạo Rating mới
            var rating = new Rating
            {
                ProductId = model.ProductId,
                MemberId = memberId,
                RatingValue = model.RatingInput,
                CreatedAt = DateTime.Now,
                Status = 1
            };
            _context.Ratings.Add(rating);
            _context.SaveChanges();

            // Tạo Comment mới
            var comment = new Comment
            {
                Id = rating.Id,
                Contents = model.CommentInput!,
                CreatedAt = DateTime.Now,
                Status = 1
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("ProductDetail", new { id = model.ProductId });
        }

    }
}

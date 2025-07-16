using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FreshFruit.Controllers
{
    public class ShopController : Controller
    {
        private readonly FreshFruitDbContext _context;

        public ShopController(FreshFruitDbContext context)
        {
            _context = context;
        }

		public IActionResult Index(
	  string? searchName,
	  List<int>? categoryIds,
	  double? priceFrom,
	  double? priceTo,
	  int page = 1,
	  int pageSize = 6)
		{
			var query = _context.Products
				.Include(p => p.Category)
				.Include(p => p.Ratings)
				.Where(p => p.Status == 1); // chỉ lấy sản phẩm còn hoạt động

			// Lọc theo tên sản phẩm
			if (!string.IsNullOrEmpty(searchName))
			{
				query = query.Where(p => p.Name.Contains(searchName));
			}

			// Lọc theo danh mục
			if (categoryIds != null && categoryIds.Any())
			{
				query = query.Where(p => categoryIds.Contains(p.CategoryId));
			}

			// Lọc theo khoảng giá
			if (priceFrom.HasValue)
			{
				query = query.Where(p => p.Price >= priceFrom.Value);
			}
			if (priceTo.HasValue)
			{
				query = query.Where(p => p.Price <= priceTo.Value);
			}
			ViewBag.Categories = _context.Categories
			.Where(c => c.Status == 1)
			.ToList();
			// Tính tổng sản phẩm sau khi lọc
			var totalProducts = query.Count();
			var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			// Lấy sản phẩm phân trang
			var products = query
				.OrderBy(p => p.Id)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var vm = new ProductListViewModel
			{
				Products = products,
				CurrentPage = page,
				TotalPages = totalPages,

				// Giữ lại điều kiện lọc để dùng lại ở View
				SearchName = searchName,
				SelectedCategoryIds = categoryIds,
				PriceFrom = priceFrom,
				PriceTo = priceTo
			};

			return View(vm);
		}

		[Route("ProductDetail/{slug}")]
        public IActionResult ProductDetail(string slug, int commentPage = 1, int commentPageSize = 4)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Ratings)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Slug == slug && p.Status == 1);

            if (product == null) return NotFound();

			var latestRatings = _context.Ratings
				.Where(r => r.ProductId == product.Id && r.Status == 1)
				.GroupBy(r => r.MemberId)
				.Select(g => g.OrderByDescending(x => x.CreatedAt).FirstOrDefault())
				.ToList();

			var comments = _context.Comments
				.Where(c => c.ProductId == product.Id && c.Status == 1)
				.ToList();

			var memberIds = comments.Select(c => c.MemberId).Distinct().ToList();
			var members = _context.Members
				.Where(m => memberIds.Contains(m.Id))
				.ToDictionary(m => m.Id, m => m.Fullname);

			var ratingsWithComments = comments.Select(c =>
			{

				var rating = latestRatings?.FirstOrDefault(r => r != null && r.MemberId == c.MemberId);
				var fullName = members.ContainsKey(c.MemberId ?? 0) ? members[c.MemberId ?? 0] : "Ẩn danh";
				Console.WriteLine($"full name: {fullName}");
				return new RatingWithCommentVM
				{
					RatingId = rating?.Id ?? 0,
					ProductId = c.ProductId,
					MemberId = c.MemberId ?? 0,
					RatingValue = rating?.RatingValue ?? 0,
					CreatedAt = rating?.CreatedAt ?? c.CreatedAt,
					Status = rating?.Status ?? 1,
					CommentText = c.Contents,
					CommentCreatedAt = c.CreatedAt,
					CommentStatus = c.Status,
					FullName = fullName
				};
			}).ToList();
			var totalComments = ratingsWithComments.Count;
			var totalCommentPages = (int)Math.Ceiling((double)totalComments / commentPageSize);
			var paginatedComments = ratingsWithComments
				.OrderByDescending(c => c.CommentCreatedAt)
				.Skip((commentPage - 1) * commentPageSize)
				.Take(commentPageSize)
				.ToList();
			var vm = new ProductDetailViewModel
            {
                Product = product,
                ProductImages = product.ProductImages?.ToList(),
                RatingsWithComments = paginatedComments,
                RelatedProducts = _context.Products
                    .Where(p => p.CategoryId == product.CategoryId && p.Slug != slug && p.Status == 1)
                    .Take(4).ToList(),
                AverageRating = ratingsWithComments.Any()
                    ? ratingsWithComments.Average(r => r.RatingValue)
                    : 0,
				CommentCurrentPage = commentPage,
				CommentTotalPages = totalCommentPages

			};
			ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToListAsync();
			return View(vm);
        }

        [HttpPost]
        public IActionResult SubmitRating(ProductDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
						
                model.Product = _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == model.ProductId);
				model.slug=model.slug ?? string.Empty;
				model.ProductImages = _context.ProductImages
                    .Where(pi => pi.ProductId == model.ProductId).ToList();

				model.RatingsWithComments = (
					from r in _context.Ratings
					join m in _context.Members on r.MemberId equals m.Id into rm
					from mem in rm.DefaultIfEmpty()
					join c in _context.Comments
					on new { r.ProductId, r.MemberId } equals new { c.ProductId, c.MemberId }
					where r.ProductId == model.ProductId && r.Status == 1 && c.Status == 1
					select new RatingWithCommentVM
					{
						RatingId = r.Id,
						ProductId = r.ProductId,
						MemberId = r.MemberId,
						RatingValue = r.RatingValue,
						CreatedAt = r.CreatedAt,
						Status = r.Status,
						CommentText = c.Contents,
						CommentCreatedAt = c.CreatedAt,
						CommentStatus = c.Status,
						FullName = mem != null ? mem.Fullname : "Ẩn danh"
	}
).ToList();


				model.RelatedProducts = _context.Products
                    .Where(p => p.CategoryId == model.Product!.CategoryId && p.Id != model.ProductId && p.Status == 1)
                    .Take(4).ToList();

                model.AverageRating = model.RatingsWithComments.Any()
                    ? model.RatingsWithComments.Average(r => r.RatingValue)
                    : 0;

                return View("ProductDetail", model);
            }
			var accountId = HttpContext.Session.GetInt32("AccountId");
			if (accountId == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var member = _context.Members.FirstOrDefault(m => m.AccountId == accountId);
			if (member == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var memberId = member.Id;
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
                ProductId=model.ProductId,
                Contents = model.CommentInput!,
                MemberId = memberId,
                CreatedAt = DateTime.Now,
                Status = 1
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("ProductDetail", new { slug= model.slug});
        }

    }
}

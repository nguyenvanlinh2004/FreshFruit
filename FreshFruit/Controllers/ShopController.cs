﻿using FreshFruit.Models;
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
        public IActionResult ProductDetail(string slug)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Ratings)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Slug == slug && p.Status == 1);

            if (product == null) return NotFound();

            var ratingsWithComments = (
                from r in _context.Ratings
				join m in _context.Members on r.MemberId equals m.Id into rm
				from member in rm.DefaultIfEmpty()
				join c in _context.Comments
                    on new { r.ProductId, r.MemberId } equals new { c.ProductId, c.MemberId }
                where r.ProductId == product.Id && r.Status == 1 && c.Status == 1
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
					 FullName = member != null ? member.Fullname : "Ẩn danh"
				}
            ).ToList();

            var vm = new ProductDetailViewModel
            {
                Product = product,
                ProductImages = product.ProductImages?.ToList(),
                RatingsWithComments = ratingsWithComments,
                RelatedProducts = _context.Products
                    .Where(p => p.CategoryId == product.CategoryId && p.Slug != slug && p.Status == 1)
                    .Take(4).ToList(),
                AverageRating = ratingsWithComments.Any()
                    ? ratingsWithComments.Average(r => r.RatingValue)
                    : 0
            };
			ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToListAsync();
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
				model.slug=model.slug ?? string.Empty;
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

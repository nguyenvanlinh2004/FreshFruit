using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshFruit.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FreshFruit.Controllers
{
	public class BlogController : Controller
	{
		private readonly FreshFruitDbContext _context;
		private const int PageSize = 6;

		public BlogController(FreshFruitDbContext context)
		{
			_context = context;
		}

		// Hàm loại bỏ dấu tiếng Việt
		public static string RemoveDiacritics(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;

			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in normalized)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}

			return sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
		}

		public IActionResult Index(string searchTitle, string searchAuthor, string searchDate, int page = 1)
		{
			var blogsQuery = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.AsQueryable();

			// Tìm theo ngày đăng (có thể giữ trong database)
			if (!string.IsNullOrEmpty(searchDate))
			{
				if (DateTime.TryParse(searchDate, out DateTime dt))
				{
					var startDate = dt.Date;
					var endDate = startDate.AddDays(1);
					blogsQuery = blogsQuery.Where(b => b.CreatedAt.HasValue
						&& b.CreatedAt.Value >= DateOnly.FromDateTime(startDate)
						&& b.CreatedAt.Value < DateOnly.FromDateTime(endDate));
				}
			}

			// Chuyển dữ liệu ra bộ nhớ để xử lý tiếng Việt
			var blogsList = blogsQuery.AsEnumerable();

			// Tìm theo tiêu đề (bỏ dấu, không phân biệt hoa thường)
			if (!string.IsNullOrEmpty(searchTitle))
			{
				var keyword = RemoveDiacritics(searchTitle);
				blogsList = blogsList.Where(b => RemoveDiacritics(b.Title).Contains(keyword));
			}

			// Tìm theo tên người viết (bỏ dấu, không phân biệt hoa thường)
			if (!string.IsNullOrEmpty(searchAuthor))
			{
				var authorKeyword = RemoveDiacritics(searchAuthor);
				blogsList = blogsList.Where(b => b.Author.Members.Any(m => RemoveDiacritics(m.Fullname).Contains(authorKeyword)));
			}

			blogsList = blogsList.OrderByDescending(b => b.CreatedAt);

			int totalBlogs = blogsList.Count();
			int totalPages = (int)Math.Ceiling((double)totalBlogs / PageSize);

			var blogs = blogsList
				.Skip((page - 1) * PageSize)
				.Take(PageSize)
				.ToList();

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;

			// Giữ lại giá trị tìm kiếm để hiển thị lại trong form
			ViewBag.SearchTitle = searchTitle;
			ViewBag.SearchAuthor = searchAuthor;
			ViewBag.SearchDate = searchDate ?? "";

			return View(blogs);
		}

		public IActionResult Details(int id)
		{
			var blog = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.FirstOrDefault(b => b.Id == id);

			if (blog == null)
			{
				// Nếu không tìm thấy bài viết, chuyển về trang danh sách và thông báo lỗi
				TempData["ErrorMessage"] = "Bài viết không tồn tại hoặc đã bị xóa!";
				return RedirectToAction("Index");
			}

			return View(blog);
		}

	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshFruit.Models;
using System.Globalization;
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

		private string GenerateSlug(string title)
		{
			if (string.IsNullOrWhiteSpace(title)) return "";
			string slug = RemoveDiacritics(title).Replace(" ", "-");
			slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-zA-Z0-9\-]", "").ToLower();
			return slug;
		}

		public IActionResult Index(string searchTitle, string searchAuthor, string searchDate, int page = 1)
		{
			var blogsQuery = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.AsQueryable();

			if (!string.IsNullOrEmpty(searchDate) && DateTime.TryParse(searchDate, out DateTime dt))
			{
				var startDate = dt.Date;
				var endDate = startDate.AddDays(1);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt.HasValue
					&& b.CreatedAt.Value >= DateOnly.FromDateTime(startDate)
					&& b.CreatedAt.Value < DateOnly.FromDateTime(endDate));
			}

			var blogsList = blogsQuery.AsEnumerable();

			if (!string.IsNullOrEmpty(searchTitle))
			{
				var keyword = RemoveDiacritics(searchTitle);
				blogsList = blogsList.Where(b => RemoveDiacritics(b.Title).Contains(keyword));
			}

			if (!string.IsNullOrEmpty(searchAuthor))
			{
				var authorKeyword = RemoveDiacritics(searchAuthor);
				blogsList = blogsList.Where(b => b.Author.Members.Any(m => RemoveDiacritics(m.Fullname).Contains(authorKeyword)));
			}

			blogsList = blogsList.OrderByDescending(b => b.CreatedAt);
			int totalBlogs = blogsList.Count();
			int totalPages = (int)Math.Ceiling((double)totalBlogs / PageSize);
			var blogs = blogsList.Skip((page - 1) * PageSize).Take(PageSize).ToList();

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;
			ViewBag.SearchTitle = searchTitle;
			ViewBag.SearchAuthor = searchAuthor;
			ViewBag.SearchDate = searchDate ?? "";

			return View(blogs);
		}

		[Route("bai-viet/{id:int}/{slug}")]
		public IActionResult Details(int id, string slug)
		{
			var blog = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.FirstOrDefault(b => b.Id == id);

			if (blog == null || blog.Slug?.ToLower() != slug.ToLower())
			{
				TempData["ErrorMessage"] = "Bài viết không tồn tại hoặc đã bị xoá!";
				return RedirectToAction("Index");
			}

			return View(blog);
		}
	}
}
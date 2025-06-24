using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BlogController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public BlogController(FreshFruitDbContext context)
		{
			_context = context;
		}

		private string RemoveDiacritics(string text)
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

		public async Task<IActionResult> Index(string searchTitle, string searchAuthor, DateTime? fromDate, DateTime? toDate, int page = 1)
		{
			int pageSize = 10;

			var blogsQuery = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.OrderByDescending(b => b.CreatedAt)
				.AsQueryable();

			if (fromDate.HasValue)
			{
				var fromDateOnly = DateOnly.FromDateTime(fromDate.Value);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt >= fromDateOnly);
			}

			if (toDate.HasValue)
			{
				var toDateOnly = DateOnly.FromDateTime(toDate.Value);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt <= toDateOnly);
			}

			var blogsList = await blogsQuery.ToListAsync();

			if (!string.IsNullOrEmpty(searchTitle))
			{
				var searchTitleNoDiacritics = RemoveDiacritics(searchTitle);
				blogsList = blogsList.Where(b => !string.IsNullOrEmpty(b.Title) &&
					RemoveDiacritics(b.Title).Contains(searchTitleNoDiacritics)).ToList();
			}

			if (!string.IsNullOrEmpty(searchAuthor))
			{
				var searchAuthorNoDiacritics = RemoveDiacritics(searchAuthor);
				blogsList = blogsList.Where(b => b.Author != null &&
					b.Author.Members.Any(m => !string.IsNullOrEmpty(m.Fullname) &&
						RemoveDiacritics(m.Fullname).Contains(searchAuthorNoDiacritics))
				).ToList();
			}

			int totalItems = blogsList.Count;
			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			var blogsPaged = blogsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;
			ViewBag.SearchTitle = searchTitle;
			ViewBag.SearchAuthor = searchAuthor;
			ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
			ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

			return View(blogsPaged);
		}

		
	}
}

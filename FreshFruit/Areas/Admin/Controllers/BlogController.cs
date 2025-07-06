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

		// ❇️ Xóa dấu tiếng Việt để tìm kiếm không dấu
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

		// ✅ Danh sách bài viết + Tìm kiếm theo ngày, tiêu đề, tác giả
		public async Task<IActionResult> Index(string searchTitle, string searchAuthor, DateTime? fromDate, DateTime? toDate)
		{
			var blogsQuery = _context.Blogs
				.Include(b => b.Author)
					.ThenInclude(a => a.Members)
				.AsQueryable();

			// 🔎 Lọc theo ngày
			if (fromDate.HasValue && toDate.HasValue)
			{
				var from = DateOnly.FromDateTime(fromDate.Value);
				var to = DateOnly.FromDateTime(toDate.Value);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt >= from && b.CreatedAt <= to);
			}
			else if (fromDate.HasValue)
			{
				var from = DateOnly.FromDateTime(fromDate.Value);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt >= from);
			}
			else if (toDate.HasValue)
			{
				var to = DateOnly.FromDateTime(toDate.Value);
				blogsQuery = blogsQuery.Where(b => b.CreatedAt <= to);
			}

			var blogs = await blogsQuery.ToListAsync();

			// 🔍 Lọc theo tiêu đề (không dấu)
			if (!string.IsNullOrEmpty(searchTitle))
			{
				var searchTitleNoDiacritics = RemoveDiacritics(searchTitle);
				blogs = blogs.Where(b =>
					!string.IsNullOrEmpty(b.Title) &&
					RemoveDiacritics(b.Title).Contains(searchTitleNoDiacritics)
				).ToList();
			}

			// 🔍 Lọc theo tên tác giả (không dấu)
			if (!string.IsNullOrEmpty(searchAuthor))
			{
				var searchAuthorNoDiacritics = RemoveDiacritics(searchAuthor);
				blogs = blogs.Where(b =>
					b.Author != null &&
					b.Author.Members.Any(m =>
						!string.IsNullOrEmpty(m.Fullname) &&
						RemoveDiacritics(m.Fullname).Contains(searchAuthorNoDiacritics)
					)
				).ToList();
			}

			return View(blogs);
		}

		//  Hiển thị form tạo
		public IActionResult Create() => View();

		//  Xử lý tạo mới
		[HttpPost]
		public async Task<IActionResult> Create(BlogViewModel model)
		{
			if (ModelState.IsValid)
			{
				var blog = new Blog
				{
					Title = model.Title,
					Slug = model.Slug,
					Contents = model.Contents,
					Image = model.Image,
					AuthorId = model.AuthorId,
					CreatedAt = DateOnly.FromDateTime(DateTime.Now),
					Status = 1
				};

				_context.Add(blog);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return View(model);
		}

		// ✅ Hiển thị form sửa
		public async Task<IActionResult> Edit(int id)
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog == null) return NotFound();

			var model = new BlogViewModel
			{
				Id = blog.Id,
				Title = blog.Title,
				Slug = blog.Slug,
				Contents = blog.Contents,
				Image = blog.Image,
				AuthorId = blog.AuthorId ?? 0,
				Status = blog.Status ?? 1
			};

			return View(model);
		}

		// ✅ Xử lý cập nhật
		[HttpPost]
		public async Task<IActionResult> Edit(BlogViewModel model)
		{
			if (ModelState.IsValid)
			{
				var blog = await _context.Blogs.FindAsync(model.Id);
				if (blog == null) return NotFound();

				blog.Title = model.Title;
				blog.Slug = model.Slug;
				blog.Contents = model.Contents;
				blog.Image = model.Image;
				blog.AuthorId = model.AuthorId;
				blog.Status = model.Status;

				_context.Update(blog);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return View(model);
		}

		// ✅ Ẩn bài viết
		public async Task<IActionResult> Hide(int id)
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog != null)
			{
				blog.Status = 0;
				_context.Update(blog);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

		// ✅ Chuyển trạng thái hiển thị
		public async Task<IActionResult> ToggleStatus(int id)
		{
			var blog = await _context.Blogs.FindAsync(id);
			if (blog == null) return NotFound();

			blog.Status = blog.Status == 1 ? 0 : 1;

			_context.Update(blog);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}

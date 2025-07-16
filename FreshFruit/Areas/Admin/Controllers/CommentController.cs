using FreshFruit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CommentController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public CommentController(FreshFruitDbContext context)
		{
			_context = context;
		}

		// Danh sách bình luận
		public IActionResult Index()
		{
			var comments = _context.Comments
				.Include(c => c.Product)
				.Include(c => c.Member)
				.OrderByDescending(c => c.CreatedAt)
				.ToList();

			return View(comments);
		}

		// Đổi trạng thái (Hiển thị / Ẩn)
		[HttpPost]
		public IActionResult ToggleStatus(int id)
		{
			var comment = _context.Comments.Find(id);
			if (comment == null)
				return NotFound();

			comment.Status = comment.Status == 1 ? 0 : 1;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}

using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
	
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly FreshFruitDbContext _context;
		public UserController(FreshFruitDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var users = _context.Accounts
				.Include(a => a.Members)
				.Select(a => new UserViewModel
				{
					AccountId = a.Id,
					Email = a.Email,
					Role = a.Role,
					AccountStatus = a.Status,
					Fullname = a.Members.FirstOrDefault().Fullname,
					Phone = a.Members.FirstOrDefault().Phone,
					Address = a.Members.FirstOrDefault().Address,
					MemberEmail = a.Members.FirstOrDefault().Email,
					Dob = a.Members.FirstOrDefault().Dob,
					MemberStatus = a.Members.FirstOrDefault().Status
				})
				.ToList();

			return View(users);
		}
		public IActionResult Details(int id)
		{
			var acc = _context.Accounts
				.Include(a => a.Members)
				.FirstOrDefault(a => a.Id == id);

			if (acc == null)
			{
				return NotFound();
			}

			var user = new UserViewModel
			{
				AccountId = acc.Id,
				Email = acc.Email,
				Role = acc.Role,
				AccountStatus = acc.Status,
				Fullname = acc.Members.FirstOrDefault()?.Fullname,
				Phone = acc.Members.FirstOrDefault()?.Phone,
				Address = acc.Members.FirstOrDefault()?.Address,
				MemberEmail = acc.Members.FirstOrDefault()?.Email,
				Dob = acc.Members.FirstOrDefault()?.Dob,
				MemberStatus = acc.Members.FirstOrDefault()?.Status
			};

			return View(user);
		}
		[HttpPost]
		public IActionResult Lock(int id)
		{
			var account = _context.Accounts.Find(id);
			if (account == null) return NotFound();

			account.Status = 0; // Khóa tài khoản
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Unlock(int id)
		{
			var account = _context.Accounts.Find(id);
			if (account == null) return NotFound();

			account.Status = 1; // Mở lại tài khoản
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}

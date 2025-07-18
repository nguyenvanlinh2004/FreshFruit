using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public AccountController(FreshFruitDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Login(LogInViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var account = _context.Accounts
				.Where(p=>p.Status==1)
				.FirstOrDefault(a => a.Email == model.UserName && a.Password == model.Password && a.Role == 1);

			if (account != null)
			{
				HttpContext.Session.SetInt32("AdminId", account.Id);
				HttpContext.Session.SetString("AdminEmail", account.Email);
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
			return View(model);
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}

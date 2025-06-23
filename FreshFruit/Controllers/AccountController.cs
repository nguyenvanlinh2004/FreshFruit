using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Controllers
{
    public class AccountController : Controller
    {
        private readonly FreshFruitDbContext _context;

        public AccountController(FreshFruitDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            {
                bool userExists = _context.Accounts.Any(a => a.Email == model.Email);
                if (userExists)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại.");
                    return View(model);
                }
                var account = new Account
                {
                    Email = model.Email!,
                    Password = model.Password!, // Nên mã hóa nhé!
                    Role = 0,
                    Status = 1
                };

                _context.Accounts.Add(account);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                // Chuyển đến trang login
                return RedirectToAction("Login");
            }
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Accounts
                            .FirstOrDefault(a => a.Email == model.UserName && a.Password == model.Password && a.Status == 1);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("AccountId", user.Id);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetInt32("Role", user.Role);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng hoặc đã bị khóa.");
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xoá toàn bộ session
            return RedirectToAction("Login");
        }

		// Hiển thị form Profile
		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			var accountId = HttpContext.Session.GetInt32("AccountId");
			if (accountId == null)
			{
				return RedirectToAction("Login");
			}

			var account = await _context.Accounts
				.Include(a => a.Members)
					.ThenInclude(m => m.Invoices)
						.ThenInclude(i => i.InvoiceDetails)
							.ThenInclude(d => d.Product)
				.FirstOrDefaultAsync(a => a.Id == accountId);

			if (account == null)
			{
				return NotFound();
			}

			var member = account.Members.FirstOrDefault();
			ViewBag.Member = member;

			if (member != null)
			{
				ViewBag.Invoices = member.Invoices.OrderByDescending(i => i.InvoiceDate).ToList();
			}
			else
			{
				ViewBag.Invoices = new List<Invoice>();
			}

			return View(account);
		}

		// Xử lý cập nhật Profile
		[HttpPost]
		public async Task<IActionResult> Profile(int id, string fullname, string phone, string address, DateTime? dob, string currentPassword, string password, string confirmPassword)
		{
			var account = await _context.Accounts.Include(a => a.Members).FirstOrDefaultAsync(a => a.Id == id);

			if (account == null)
			{
				return NotFound();
			}

			// Kiểm tra mật khẩu hiện tại
			if (account.Password != currentPassword)
			{
				TempData["Error"] = "Mật khẩu hiện tại không đúng.";
				return RedirectToAction(nameof(Profile));
			}

			// Nếu người dùng muốn đổi mật khẩu
			if (!string.IsNullOrEmpty(password))
			{
				if (password == currentPassword)
				{
					TempData["Error"] = "Mật khẩu mới không được trùng với mật khẩu hiện tại.";
					return RedirectToAction(nameof(Profile));
				}

				if (string.IsNullOrEmpty(confirmPassword))
				{
					TempData["Error"] = "Vui lòng nhập lại mật khẩu mới.";
					return RedirectToAction(nameof(Profile));
				}

				if (password != confirmPassword)
				{
					TempData["Error"] = "Mật khẩu mới và mật khẩu nhập lại không khớp.";
					return RedirectToAction(nameof(Profile));
				}

				account.Password = password; 
				_context.Update(account);
			}

			// Cập nhật thông tin thành viên
			var member = account.Members.FirstOrDefault();
			if (member != null)
			{
				member.Fullname = fullname;
				member.Phone = phone;
				member.Address = address;
				member.Dob = dob.HasValue ? DateOnly.FromDateTime(dob.Value) : (DateOnly?)null;

				_context.Update(member);
			}

			await _context.SaveChangesAsync();
			TempData["Success"] = "Cập nhật thông tin thành công.";

			return RedirectToAction(nameof(Profile));
		}



	}
}

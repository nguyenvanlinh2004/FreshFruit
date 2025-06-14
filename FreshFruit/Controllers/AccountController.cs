using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
    }
}

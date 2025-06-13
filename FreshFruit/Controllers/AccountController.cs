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
            //if(ModelState.IsValid)
            //{
            //    bool userExists= _context.Accounts.Any(a=>a.UserName==model.UserName);
            //    if(userExists) 
            //    {
            //        ModelState.AddModelError("Email", "Email đã tồn tại.");
            //        return View(model);
            //    }
            //}

            return View(model);
        }
        public IActionResult LogIn()
        {
            return View();
        }
    }
}

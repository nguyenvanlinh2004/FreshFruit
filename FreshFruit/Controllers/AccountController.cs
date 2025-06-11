using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}

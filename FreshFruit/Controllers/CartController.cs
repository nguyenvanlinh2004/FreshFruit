using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}

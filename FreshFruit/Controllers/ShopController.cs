using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

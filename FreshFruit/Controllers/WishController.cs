using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class WishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

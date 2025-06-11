using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

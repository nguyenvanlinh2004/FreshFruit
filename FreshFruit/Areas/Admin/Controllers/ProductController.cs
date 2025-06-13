using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }


    }
}

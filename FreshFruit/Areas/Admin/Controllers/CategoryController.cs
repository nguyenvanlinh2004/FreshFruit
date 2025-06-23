using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly FreshFruitDbContext _context;
        public CategoryController(FreshFruitDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cat = _context.Categories.ToList();
            return View(cat);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cat = new Category
            {
                Name = model.Name,
                Status = 1
            };

            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thêm loại thành công!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cat = await _context.Categories.FindAsync(model.Id);
            if (cat == null)
            {
                return NotFound();
            }

            cat.Name = model.Name;
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật loại thành công!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            cat.Status = 0;
            await _context.SaveChangesAsync();
            TempData["success"] = "Đã xóa loại sản phẩm!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Restore(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            cat.Status = 1;
            await _context.SaveChangesAsync();
            TempData["success"] = "Khôi phục loại sản phẩm thành công!";
            return RedirectToAction("Index");
        }
    }
}

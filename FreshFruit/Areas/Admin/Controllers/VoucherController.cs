using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private readonly FreshFruitDbContext _context;

        public VoucherController(FreshFruitDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Voucher
        public async Task<IActionResult> Index()
        {
            var vouchers = await _context.Vouchers.ToListAsync();
            return View(vouchers);
        }

        // GET: /Admin/Voucher/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Voucher/Create
        [HttpPost]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                return View(voucher);
            }

            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();

            TempData["success"] = "Thêm voucher thành công!";
            return RedirectToAction("Index");
        }

        // GET: /Admin/Voucher/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: /Admin/Voucher/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Voucher voucher)
        {
            if (id != voucher.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(voucher);
            }

            _context.Update(voucher);
            await _context.SaveChangesAsync();

            TempData["success"] = "Cập nhật voucher thành công!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();

            TempData["success"] = "Xoá voucher thành công!";
            return RedirectToAction("Index");
        }
    }

}


using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseReceiptController : Controller
    {
        private readonly FreshFruitDbContext _context;
        public PurchaseReceiptController(FreshFruitDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var receipts = await _context.PurchaseReceipts
                .Include(r => r.CreateByNavigation)
                .OrderByDescending(r => r.ReceiptDate)
                .ToListAsync();

            return View(receipts);
        }
        public async Task<IActionResult> Details(int id)
        {
            var receipt = await _context.PurchaseReceipts
                .Include(r => r.CreateByNavigation)
                .Include(r => r.PurchaseReceiptDetails)
                    .ThenInclude(d => d.Product)
                    .Where(s=>s.Status==1)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products
                .Where(p => p.Status == 1)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PurchaseReceiptCreateViewModel vm)
        {
            if (vm.Items == null || !vm.Items.Any())
            {
                ModelState.AddModelError("", "Bạn phải chọn ít nhất 1 sản phẩm.");
                return View(vm);
            }

            var receipt = new PurchaseReceipt
            {
                Supplier = vm.Supplier,
                ReceiptDate = vm.ReceiptDate,
                CreateBy = 1, // bạn có thể dùng User.Identity nếu đã có auth
                Status = 1,
                ShipmentId = $"PNH-{DateTime.Now:yyyyMMddHHmmss}"
            };
            _context.PurchaseReceipts.Add(receipt);
            await _context.SaveChangesAsync(); // để có receipt.Id

            decimal total = 0;

            foreach (var item in vm.Items)
            {
                var itemTotal = item.ImportPrice * item.Quantity;
                total += itemTotal;

                var detail = new PurchaseReceiptDetail
                {
                    ReceiptId = receipt.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ImportPrice = item.ImportPrice,
                    ExpiryDate = item.ExpiryDate,
                    Total = itemTotal,
                    Status = 1
                };

                // Tăng tồn kho sản phẩm
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                    product.ShipmentId = receipt.ShipmentId;
                }

                _context.PurchaseReceiptDetails.Add(detail);
            }

            receipt.Total = (double?)total;
            await _context.SaveChangesAsync();

            TempData["success"] = "Thêm phiếu nhập thành công!";
            return RedirectToAction("Index");
        }

    }
}

using FreshFruit.Helpers;
using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace FreshFruit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly FreshFruitDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(FreshFruitDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var warningDate = today.AddDays(7); // lấy các lô hết hạn trong 7 ngày tới

            var data = await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductWithExpiryViewModel
                {
                    Product = p,
                    ExpiringLots = p.PurchaseReceiptDetails
                        .Where(d => d.ExpiryDate != null
                                    && d.ExpiryDate <= warningDate
                                    && d.Quantity > 0)
                        .OrderBy(d => d.ExpiryDate)
                        .ToList()
                }).ToListAsync();

            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories
                                  .Where(c => c.Status == 1)
                                  .Select(c => new { c.Id, c.Name })
                                  .ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                return View(vm);
            }

            var slug = SlugHelper.GenerateSlug(vm.Name);

            var product = new Product
            {
                Name = vm.Name,
                CategoryId = vm.CategoryId,
                Price = vm.Price,
                Description = vm.Description,
                LongDescription=vm.LongDescription,
                Quantity = 0,
                ShipmentId =null,
                Slug = slug,
                Status = 1
            }; ;

            if (vm.ImageFile != null)
            {
                try
                {
                    product.Image = await ImageHelper.SaveAndResizeImageAsync(vm.ImageFile, _env, slug, "images", 600);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu ảnh chính: " + ex.Message);
                    ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                    return View(vm);
                }
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (vm.ImageFiles != null && vm.ImageFiles.Any())
            {
                foreach (var file in vm.ImageFiles)
                {
                    try
                    {
                        var fileName = await ImageHelper.SaveAndResizeImageAsync(file, _env, slug, "images", 600);
                        _context.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = fileName,
                            Status = 1
                        });
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi lưu ảnh chính: " + ex.Message);
                        ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                        return View(vm);
                    }
                }

                await _context.SaveChangesAsync();
            }

            TempData["success"] = "Thêm sản phẩm thành công!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && p.Status == 1);

            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                LongDescription = product.LongDescription,
                Slug = product.Slug,
                Image = product.Image,
                OldGalleryImages = product.ProductImages
                                    .Where(i => i.Status == 1)
                                    .Select(i => i.ImageUrl).ToList()
            };

            ViewBag.Categories = _context.Categories
                                  .Where(c => c.Status == 1)
                                  .ToList();

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                return View(vm);
            }

            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Console.WriteLine(new { des = vm.Description, longdes = vm.LongDescription });
            // Cập nhật thông tin
            product.Name = vm.Name;
            product.CategoryId = vm.CategoryId;
            product.Price = vm.Price;
            product.Description = vm.Description;
            product.LongDescription = vm.LongDescription;
            product.Slug = SlugHelper.GenerateSlug(vm.Name);

            if (vm.ImageFile != null)
            {
                try
                {
                    product.Image = await ImageHelper.SaveAndResizeImageAsync(vm.ImageFile, _env, product.Slug, "images", 600);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi lưu ảnh chính: " + ex.Message);
                    ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                    return View(vm);
                }
            }

            if (vm.ImageFiles != null && vm.ImageFiles.Any())
            {
                foreach (var file in vm.ImageFiles)
                {
                    try
                    {
                        var fileName = await ImageHelper.SaveAndResizeImageAsync(file, _env, product.Slug, "images", 600);
                        _context.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = fileName,
                            Status = 1
                        });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi lưu ảnh phụ: " + ex.Message);
                        ViewBag.Categories = _context.Categories.Where(c => c.Status == 1).ToList();
                        return View(vm);
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["success"] = "Cập nhật sản phẩm thành công!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Status = 0;
            await _context.SaveChangesAsync();

            TempData["success"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Status = 1;
            await _context.SaveChangesAsync();

            TempData["success"] = "Khôi phục sản phẩm thành công!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.PurchaseReceiptDetails)
                    .ThenInclude(d => d.Receipt)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var lots = product.PurchaseReceiptDetails
                .Where(d => d.Status == 1) // nếu có Status để kiểm soát
                .OrderBy(d => d.ExpiryDate)
                .Select(d => new LotViewModel
                {
                    Quantity = d.Quantity ?? 0,
                    ImportPrice = d.ImportPrice ?? 0,
                    ExpiryDate = d.ExpiryDate,
                    Total = d.Total ?? 0,
                    ShipmentId = d.Receipt.ShipmentId
                }).ToList();

            var vm = new ProductLotDetailVM
            {
                Product = product,
                Lots = lots
            };

            return View(vm);
        }

    }
}


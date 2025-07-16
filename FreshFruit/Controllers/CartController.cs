using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using FreshFruit.Services;
using FreshFruit.Services.Momo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace FreshFruit.Controllers
{
    public class CartController : Controller
    {
        private readonly FreshFruitDbContext _context;
        private readonly ICartService _cartService;
        private readonly IProductServices _productServices;
        private readonly IInvoiceServices _invoiceServices;
        private readonly IMomoService _momoService;
        private readonly int pageSize = 4;
        public CartController(FreshFruitDbContext context, ICartService cartService, IProductServices productServices, IInvoiceServices invoiceServices, IMomoService momoService)
        {
            _cartService = cartService;
            _context = context;
            _productServices = productServices;
            _invoiceServices = invoiceServices;
            _momoService = momoService;
        }
        public IActionResult Index()
        {
            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems,
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };

            return View(cartViewModel);
        }

        [HttpGet]
        public IActionResult GetCart(int currentPage = 1)
        {
            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };

            return Json(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, decimal quantity)
        {
            var product = await _productServices.GetProduct(productId);

            bool isAddToCart = _cartService.AddToCart(product, quantity);

            if (!isAddToCart)
            {
                return Json(new { success = false });
            }
            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems,
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };

            return Json(new { success = true, cartViewModel = cartViewModel });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var product = await _productServices.GetProduct(productId);

            if (product != null)
            {
                _cartService.RemoveFromCart(product);

                return Json(new { success = true, newTotalPrice = _cartService.GetTotalPrice() });
            }

            return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng." });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            bool isClearCart = _cartService.ClearCart();

            if (!isClearCart)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        public IActionResult IncreaseQuantity(int cartItemId)
        {
            if (!_cartService.IncreaseQuantity(cartItemId))
            {
                return Json(new { success = false });
            }

            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems,
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };
            return Json(new { success = true, cartViewModel = cartViewModel });
        }
        public IActionResult DecreaseQuantity(int cartItemId)
        {
            if (!_cartService.DecreaseQuantity(cartItemId))
            {
                return Json(new { success = false });
            }
            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems,
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };
            return Json(new { success = true, cartViewModel = cartViewModel });
        }
        public IActionResult UpdateQuantity(int cartItemId, decimal quantity)
        {
            if (!_cartService.UpdateQuantity(cartItemId, quantity))
            {
                return Json(new { success = false });
            }
            var cartViewModel = new CartViewModel
            {
                CartItems = _cartService.GetCart().CartItems,
                TotalPrice = _cartService.GetTotalPrice(),
                TotalQuantity = _cartService.GetTotalQuantity(),
                TotalItems = _cartService.GetTotalItems()
            };
            return Json(new { success = true, cartViewModel = cartViewModel });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var member = _context.Members.FirstOrDefault(m => m.AccountId == accountId);
            if (member == null || string.IsNullOrWhiteSpace(member.Fullname) || string.IsNullOrWhiteSpace(member.Phone) || string.IsNullOrWhiteSpace(member.Address))
            {
                TempData["error"] = "Vui lòng cập nhật đầy đủ thông tin trước khi thanh toán.";
                return RedirectToAction("Profile", "Account");
            }
            var now = DateTime.Now;
            var validVouchers = _context.Vouchers
                .Where(v => v.Status == 1 && now >= v.StartDate && now <= v.EndDate)
                .ToList();
            var cartItems = _cartService.GetCart().CartItems;
            var totalPrice = _cartService.GetTotalPrice();
            var checkoutVM = new CheckOutViewModel
            {
                invoice = new Invoice
                {
                    MemberId = member.Id,
                    Email = member.Email,
                },
                CartItems = cartItems,
                TotalPrice = totalPrice,
                FullName = member.Fullname,
                Phone = member.Phone,
                Address = member.Address,
                Vouchers=validVouchers
			};
			Console.WriteLine($"sdyudskjlfv {member.Email}");
			return View(checkoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutViewModel checkout)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            var cartItems = _cartService.GetCart().CartItems;
			var member = await _context.Members.FirstOrDefaultAsync(m => m.AccountId == accountId.Value);
			if (member == null)
			{
				return BadRequest(new { success = false, message = "Không tìm thấy thông tin thành viên." });
			}
			Console.WriteLine($"dyigsfodjvpgbkopfjioehduif{accountId}");
            if (accountId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.Remove("invoice.InvoicesCode");
            ModelState.Remove("invoice.Member");

            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, modelState = ModelState });
            }

            var invoice = new Invoice
            {
                InvoicesCode = $"HD{DateTime.Now:yyyyMMddHHmmssfff}",
                MemberId = member.Id,
                Fullname = checkout.invoice.Fullname,
                Phone = checkout.invoice.Phone?.Trim(),
                Email = checkout.invoice.Email,
                Address = checkout.invoice.Address,
                PaymentMethod = checkout.invoice.PaymentMethod,
                Total = checkout.invoice.Total,
                Status = 0,
                InvoiceDate = DateTime.Now
            };
            _cartService.Checkout(invoice, cartItems);
            _cartService.ClearCart();

            return Json(new
            {
                success = true,
                invoice = new
                {
                    invoice.InvoicesCode,
                    invoice.Total,
                    invoice.PaymentMethod,
                    InvoiceDate = invoice.InvoiceDate.ToString("yyyy-MM-dd"),
                    invoice.Fullname,
                    invoice.Phone,
                    invoice.Address
                }
        
            });
        }
        [HttpGet]
        public  IActionResult PaymentCallBack()
        {
            var momoResponse = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(momoResponse); // trả về view có model
        }

    }
}
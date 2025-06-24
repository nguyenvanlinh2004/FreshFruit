using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using FreshFruit.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
        public class CartController : Controller
        {
            private readonly FreshFruitDbContext _context;
            private readonly ICartService _cartService;
            private readonly IProductServices _productServices;
            private readonly IInvoiceServices _invoiceServices;
            private readonly int pageSize = 4;
            public CartController(FreshFruitDbContext context,ICartService cartService, IProductServices productServices, IInvoiceServices invoiceServices)
            {
                _cartService = cartService;
                _context = context;
                _productServices = productServices;
                _invoiceServices = invoiceServices;
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
            public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
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
            public IActionResult UpdateQuantity(int cartItemId, int quantity)
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
                var cartItems = _cartService.GetCart().CartItems;
                var totalPrice = _cartService.GetTotalPrice();

                Member? member = new Member();
                var accountId = HttpContext.Session.GetInt32("AccountId");

            if (accountId == null)
            {
                return BadRequest(new { success = false, message = "Tài khoản chưa được xác định." });
            }

            var checkout = new CheckOutViewModel
                {
                    invoice = new Invoice
                    {
                        MemberId = (int)accountId!,
                        InvoiceDate= DateOnly.FromDateTime(DateTime.Now)
                    },
                    CartItems = cartItems,
                    TotalPrice = totalPrice
                };
                return View(checkout);
            }

            [HttpPost]
            public IActionResult Checkout(CheckOutViewModel checkout,int MemberId)
            {
                int? memberId = null;
                var cartItems = _cartService.GetCart().CartItems;

                ModelState.Remove("Invoices.InvoicesCode");

                if (MemberId!=null)
                {
                    memberId = _context.Members.FirstOrDefault(m=>m.Id==memberId)!.Id;
                }
                if (!ModelState.IsValid)
                {

                    return BadRequest(new { success = false, modelState = ModelState });
                }
                Invoice invoice = new Invoice
                {
                    InvoicesCode = _invoiceServices.GenerateOrderCode(),
                    InvoiceDate=DateOnly.FromDateTime(DateTime.Now),
                    PaymentMethod = checkout.invoice.PaymentMethod,
                    Total = checkout.invoice.Total,
                    MemberId= (int)memberId!,
                    Status = 1,
                };

                _cartService.Checkout(invoice, cartItems);

                _cartService.ClearCart();

                return Json(new { success = true, invoice = invoice });
            }
        }

}

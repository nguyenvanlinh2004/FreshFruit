using FreshFruit.Models;
using Newtonsoft.Json;
using FreshFruit.Services;

namespace FreshFruit.Services
{
	public class CartService : ICartService
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly FreshFruitDbContext _context;
		private readonly string cartSessionKey = "Cart";

		public CartService(IHttpContextAccessor contextAccessor, FreshFruitDbContext context)
		{
			_contextAccessor = contextAccessor;
			_context = context;
		}
		private void SaveCartToSession(Cart cart)
		{
			_contextAccessor.HttpContext!.Session.SetString(cartSessionKey, JsonConvert.SerializeObject(cart));
		}
		public Cart GetCart()
		{
			var cartSession = _contextAccessor.HttpContext!.Session.GetString(cartSessionKey);

			return string.IsNullOrEmpty(cartSession)
				? new Cart()
				: JsonConvert.DeserializeObject<Cart>(cartSession) ?? new Cart();
		}
		public bool AddToCart(Product product, decimal quantity)
		{
			Cart cart = GetCart();
			CartItem cartItem = cart.CartItems.FirstOrDefault(p => p.Product.Id == product.Id)!;

			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					CartItemId = cart.CartItems.Count() + 1,
					Product = product,
					Quantity = quantity
				};
				cart.CartItems.Add(cartItem);
			}
			else
			{
				cartItem.Quantity += quantity;

				decimal productQty = product.Quantity.HasValue ? (decimal)product.Quantity.Value : 0m;

				if (cartItem.Quantity > productQty)
				{
					return false;
				}
			}
			SaveCartToSession(cart);

			return true;
		}

		public void RemoveFromCart(Product product)
		{
			Cart cart = GetCart();
			cart.CartItems.Remove(cart.CartItems.FirstOrDefault(p => p.Product.Id == product.Id)!);

			SaveCartToSession(cart);
		}

		public bool ClearCart()
		{
			Cart cart = GetCart();

			if (cart.CartItems.Count() == 0)
			{
				return false;
			}

			cart.CartItems.Clear();
			SaveCartToSession(cart);
			return true;
		}

		public decimal GetTotalQuantity()
		{
			Cart cart = GetCart();
			return cart.CartItems.Sum(p => p.Quantity);
		}

		public int GetTotalItems()
		{
			Cart cart = GetCart();
			return cart.CartItems.Count();
		}
		public decimal GetTotalPrice()
		{
			Cart cart = GetCart();
			return cart.CartItems.Sum(p => p.Quantity * (decimal)(p.Product.Price ?? 0));
		}

		public bool IncreaseQuantity(int cartItemId)
		{
			Cart cart = GetCart();
			CartItem cartItem = cart.CartItems.FirstOrDefault(item => item.CartItemId == cartItemId) ?? new CartItem();

			// Chuyển kiểu cartItem.Product.Quantity sang decimal và xử lý null
			decimal productQuantity = cartItem.Product?.Quantity != null
				? (decimal)cartItem.Product.Quantity
				: 0m;

			if (cartItem.Quantity >= productQuantity)
			{
				return false;
			}
			cartItem.Quantity++;
			SaveCartToSession(cart);
			return true;
		}

		public bool DecreaseQuantity(int cartItemId)
		{
			Cart cart = GetCart();
			CartItem cartItem = cart.CartItems.FirstOrDefault(item => item.CartItemId == cartItemId) ?? new CartItem();

			if (cartItem.Quantity <= 0)
			{
				return false;
			}
			cartItem.Quantity--;
			SaveCartToSession(cart);
			return true;
		}

		public bool UpdateQuantity(int cartItemId, decimal quantity)
		{
			Cart cart = GetCart();
			CartItem cartItem = cart.CartItems.FirstOrDefault(item => item.CartItemId == cartItemId) ?? new CartItem();

			decimal productQuantity = cartItem.Product?.Quantity != null
			? (decimal)cartItem.Product.Quantity
			: 0m;

			if (quantity > productQuantity || quantity < 0)
			{
				return false;
			}
			cartItem.Quantity = quantity;
			SaveCartToSession(cart);
			return true;
		}

		public void Checkout(Invoice invoice, List<CartItem> cartItems)
		{

			_context.Invoices.Add(invoice);
			_context.SaveChanges();

			var detailOrders = new List<InvoiceDetail>();

			foreach (CartItem item in cartItems)
			{
				var productInDb = _context.Products.FirstOrDefault(p => p.Id == item.Product.Id);
				if (productInDb != null)
				{
					decimal currentQty = productInDb.Quantity.HasValue ? (decimal)productInDb.Quantity.Value : 0m;

					currentQty -= item.Quantity;

					if (currentQty < 0)
						currentQty = 0m;

					productInDb.Quantity = (double)currentQty; // ép lại về double nếu Quantity kiểu double?
				}
				detailOrders.Add(new InvoiceDetail
				{
					InvoiceId = invoice.Id,
					ProductId = item.Product.Id,
					Quantity = item.Quantity,
					UnitPrice = invoice.Total,
					Total = (decimal)(item.Product?.Price ?? 0) * item.Quantity,
					Status = 1
				});
			}

			_context.InvoiceDetails.AddRange(detailOrders);
			_context.SaveChanges();

			//var detailOrders = cartItems.Select(item => new DetailOrder
			//{
			//    OrderId = order.Id,
			//    ProductId = item.Product.Id,
			//    Quantity = item.Quantity,
			//    TotalPrice = item.Product.Price * item.Quantity
			//});

			//_context.DetailOrders.AddRange(detailOrders);
		}
	}
}
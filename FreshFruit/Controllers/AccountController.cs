using FreshFruit.Models;
using FreshFruit.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Controllers
{
	public class AccountController : Controller
	{
		private readonly FreshFruitDbContext _context;

		public AccountController(FreshFruitDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			bool userExists = _context.Accounts.Any(a => a.Email == model.Email);
			if (userExists)
			{
				ModelState.AddModelError("Email", "Email đã tồn tại.");
				return View(model);
			}

			var account = new Account
			{
				Email = model.Email!,
				Password = model.Password!,
				Role = 0,
				Status = 1
			};
			
			_context.Accounts.Add(account);
			_context.SaveChanges();
			var member = new Member
			{
				AccountId = account.Id,
				Fullname = "",
				Address = "",
				Phone = "",
				Email = account.Email,
				Status = 1
			};

			_context.Members.Add(member);
			_context.SaveChanges(); // Lưu lại member
			TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
			return RedirectToAction("Login");
		}

		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LogInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _context.Accounts
					.FirstOrDefault(a => a.Email == model.UserName && a.Password == model.Password && a.Status == 1);

				if (user != null)
				{
					HttpContext.Session.SetInt32("AccountId", user.Id);
					HttpContext.Session.SetString("Email", user.Email);
					HttpContext.Session.SetInt32("Role", user.Role);
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng hoặc đã bị khóa.");
			}
			return View(model);
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}

		[HttpGet]
		public async Task<IActionResult> Profile(string statusFilter, string fromDate, string toDate, int page = 1)
		{
			var accountId = HttpContext.Session.GetInt32("AccountId");
			if (accountId == null) return RedirectToAction("Login");

			var account = await _context.Accounts
				.Include(a => a.Members)
					.ThenInclude(m => m.Invoices)
						.ThenInclude(i => i.InvoiceDetails)
							.ThenInclude(d => d.Product)
				.FirstOrDefaultAsync(a => a.Id == accountId);

			if (account == null) return NotFound();

			var member = account.Members.FirstOrDefault();
			ViewBag.Member = member;

			var invoices = member?.Invoices.AsQueryable() ?? Enumerable.Empty<Invoice>().AsQueryable();

			if (!string.IsNullOrEmpty(statusFilter) && int.TryParse(statusFilter, out int status))
			{
				invoices = invoices.Where(i => i.Status == status);
			}

			if (DateTime.TryParse(fromDate, out var from))
			{
				invoices = invoices.Where(i => i.InvoiceDate.Date >= from.Date);
			}
			if (DateTime.TryParse(toDate, out var to))
			{
				invoices = invoices.Where(i => i.InvoiceDate.Date <= to.Date);
			}

			int pageSize = 10;
			int totalItems = invoices.Count();
			int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
			var pagedInvoices = invoices
				.OrderByDescending(i => i.InvoiceDate)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Invoices = pagedInvoices;
			ViewBag.StatusFilter = statusFilter;
			ViewBag.FromDate = fromDate;
			ViewBag.ToDate = toDate;
			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;

			return View(account);
		}

		[HttpPost]
		public async Task<IActionResult> Profile(int id, string email, string fullname, string phone, string address, DateTime? dob, string currentPassword, string password, string confirmPassword)
		{
			var account = await _context.Accounts.Include(a => a.Members).FirstOrDefaultAsync(a => a.Id == id);

			if (account == null)
			{
				TempData["Error"] = "Tài khoản không tồn tại.";
				return RedirectToAction(nameof(Profile));
			}

			if (account.Password != currentPassword)
			{
				TempData["Error"] = "Mật khẩu hiện tại không đúng.";
				return RedirectToAction(nameof(Profile));
			}

			if (!string.IsNullOrEmpty(password))
			{
				if (password == currentPassword)
				{
					TempData["Error"] = "Mật khẩu mới không được trùng với mật khẩu hiện tại.";
					return RedirectToAction(nameof(Profile));
				}

				if (string.IsNullOrEmpty(confirmPassword))
				{
					TempData["Error"] = "Vui lòng nhập lại mật khẩu mới.";
					return RedirectToAction(nameof(Profile));
				}

				if (password != confirmPassword)
				{
					TempData["Error"] = "Mật khẩu mới và mật khẩu nhập lại không khớp.";
					return RedirectToAction(nameof(Profile));
				}

				account.Password = password;
				_context.Update(account);
			}

			var member = account.Members.FirstOrDefault();

			if (member != null)
			{
				member.Fullname = fullname;
				member.Phone = phone;
				member.Address = address;
				member.Dob = dob.HasValue ? DateOnly.FromDateTime(dob.Value) : (DateOnly?)null;
				member.Email = email;

				_context.Update(member);
			}
			else
			{
				var newMember = new Member
				{
					Fullname = fullname,
					Phone = phone,
					Address = address,
					Dob = dob.HasValue ? DateOnly.FromDateTime(dob.Value) : (DateOnly?)null,
					AccountId = account.Id,
					Email=account.Email
					
				};

				_context.Members.Add(newMember);
			}

			await _context.SaveChangesAsync();
			TempData["Success"] = "Cập nhật thông tin thành công.";
			return RedirectToAction(nameof(Profile));
		}

		[HttpPost]
		public async Task<IActionResult> CancelOrder(int id)
		{
			var invoice = await _context.Invoices.Include(i => i.Member).FirstOrDefaultAsync(i => i.Id == id);
			var accountId = HttpContext.Session.GetInt32("AccountId");

			if (invoice == null || invoice.Member?.AccountId != accountId)
			{
				TempData["Error"] = "Không thể huỷ đơn hàng.";
				return RedirectToAction(nameof(Profile));
			}

			if (invoice.Status != 1)
			{
				TempData["Error"] = "Chỉ có thể huỷ đơn đang giao.";
				return RedirectToAction(nameof(Profile));
			}

			invoice.Status = 3; // Đã huỷ
			_context.Invoices.Update(invoice);
			await _context.SaveChangesAsync();

			TempData["Success"] = "Đã huỷ đơn hàng thành công.";
			return RedirectToAction(nameof(Profile));
		}

		[HttpGet]
		public async Task<IActionResult> InvoiceDetail(int id)
		{
			var accountId = HttpContext.Session.GetInt32("AccountId");
			if (accountId == null) return RedirectToAction("Login");

			var invoice = await _context.Invoices
				.Include(i => i.InvoiceDetails)
					.ThenInclude(d => d.Product)
				.Include(i => i.Member)
				.FirstOrDefaultAsync(i => i.Id == id);

			if (invoice == null || invoice.Member?.AccountId != accountId)
			{
				TempData["Error"] = "Không thể xem đơn hàng này.";
				return RedirectToAction("Profile");
			}

			return View("InvoiceDetail", invoice); // View đặt trong Views/Account
		}
	}
}
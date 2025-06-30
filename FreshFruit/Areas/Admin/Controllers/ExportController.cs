using DinkToPdf;
using DinkToPdf.Contracts;
using FreshFruit.Models;
using FreshFruit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ExportController : Controller
	{
		private readonly FreshFruitDbContext _context;
		private readonly IConverter _converter;
		private readonly IRazorViewToStringRenderer _viewRenderer;

		public ExportController(FreshFruitDbContext context, IConverter converter, IRazorViewToStringRenderer viewRenderer)
		{
			_context = context;
			_converter = converter;
			_viewRenderer = viewRenderer;
		}

		// === XUẤT HÓA ĐƠN (XEM HTML TRƯỚC) ===
		public IActionResult ExportInvoice(int id)
		{
			var invoice = _context.Invoices
				.Include(i => i.Member)
				.Include(i => i.InvoiceDetails).ThenInclude(d => d.Product)
				.FirstOrDefault(i => i.Id == id);

			if (invoice == null) return NotFound();

			var companyInfo = _context.CompanyInfos.FirstOrDefault();
			ViewBag.CompanyInfo = companyInfo;

			return View("InvoicePdf", invoice);
		}

		// === XUẤT PDF HÓA ĐƠN ===
		[HttpPost]
		public async Task<IActionResult> GenerateInvoicePdf(int id)
		{
			var invoice = _context.Invoices
				.Include(i => i.Member)
				.Include(i => i.InvoiceDetails).ThenInclude(d => d.Product)
				.FirstOrDefault(i => i.Id == id);

			if (invoice == null) return NotFound();

			var companyInfo = _context.CompanyInfos.FirstOrDefault();

			var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
			{
				Model = invoice
			};
			viewData["CompanyInfo"] = companyInfo;

			var htmlContent = await _viewRenderer.RenderViewToStringAsync(this, "InvoicePdf", invoice, viewData);

			var pdf = new HtmlToPdfDocument
			{
				GlobalSettings = new GlobalSettings
				{
					PaperSize = PaperKind.A4,
					Orientation = Orientation.Portrait,
					Margins = new MarginSettings { Top = 10, Bottom = 10 }
				},
				Objects = { new ObjectSettings { HtmlContent = htmlContent } }
			};

			var fileBytes = _converter.Convert(pdf);

			// Tạo thư mục nếu chưa tồn tại
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "pdfinvoice");
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			// Tạo tên file
			var fileName = $"Invoice_{id}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
			var filePath = Path.Combine(folderPath, fileName);

			// Ghi file ra đĩa
			await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

			// Trả về đường dẫn để tải/xem
			var relativePath = $"/pdf/pdfinvoice/{fileName}";
			return Redirect(relativePath); // hoặc return File(fileBytes, "application/pdf", fileName);
		}


		// === XUẤT PHIẾU NHẬP (HTML) ===
		public IActionResult ExportReceipt(int id)
		{
			var receipt = _context.PurchaseReceipts
				.Include(r => r.CreateByNavigation)
				.Include(r => r.PurchaseReceiptDetails).ThenInclude(d => d.Product)
				.FirstOrDefault(r => r.Id == id);

			if (receipt == null) return NotFound();

			var companyInfo = _context.CompanyInfos.FirstOrDefault();
			ViewBag.CompanyInfo = companyInfo;

			return View("ReceiptPdf", receipt);
		}

		// === XUẤT PDF PHIẾU NHẬP ===
		[HttpPost]
		public async Task<IActionResult> GenerateReceiptPdf(int id)
		{
			var receipt = _context.PurchaseReceipts
				.Include(r => r.CreateByNavigation)
				.Include(r => r.PurchaseReceiptDetails).ThenInclude(d => d.Product)
				.FirstOrDefault(r => r.Id == id);

			if (receipt == null) return NotFound();

			var companyInfo = _context.CompanyInfos.FirstOrDefault();

			var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
			{
				Model = receipt
			};
			viewData["CompanyInfo"] = companyInfo;

			var htmlContent = await _viewRenderer.RenderViewToStringAsync(this, "ReceiptPdf", receipt, viewData);

			var pdf = new HtmlToPdfDocument
			{
				GlobalSettings = new GlobalSettings
				{
					PaperSize = PaperKind.A4,
					Orientation = Orientation.Portrait,
					Margins = new MarginSettings { Top = 10, Bottom = 10 }
				},
				Objects = { new ObjectSettings { HtmlContent = htmlContent } }
			};

			var fileBytes = _converter.Convert(pdf);

			// Tạo thư mục nếu chưa tồn tại
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "pdfreceipt");
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			// Tạo tên file
			var fileName = $"Receipt_{id}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
			var filePath = Path.Combine(folderPath, fileName);

			// Ghi file ra đĩa
			await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

			// Trả về đường dẫn tương đối
			var relativePath = $"/pdf/pdfreceipt/{fileName}";
			return Redirect(relativePath); // hoặc return File(fileBytes, "application/pdf", fileName);
		}



	}
}

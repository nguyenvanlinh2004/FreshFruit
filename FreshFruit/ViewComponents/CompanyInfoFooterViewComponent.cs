using Microsoft.AspNetCore.Mvc;
using FreshFruit.Models;
using System.Linq;

namespace FreshFruit.ViewComponents
{
	public class CompanyInfoFooterViewComponent : ViewComponent
	{
		private readonly FreshFruitDbContext _context;

		public CompanyInfoFooterViewComponent(FreshFruitDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var info = _context.CompanyInfos.FirstOrDefault() ?? new CompanyInfo();
			return View(info); // Tự động gọi Default.cshtml
		}
	}
}

using FreshFruit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class FooterViewComponent : ViewComponent
{
	private readonly FreshFruitDbContext _context;

	public FooterViewComponent(FreshFruitDbContext context)
	{
		_context = context;
	}

	public IViewComponentResult Invoke()
	{
		var companyInfo = _context.CompanyInfos.FirstOrDefault();

		if (companyInfo == null)
		{
			// Khởi tạo dữ liệu mặc định để tránh null
			companyInfo = new CompanyInfo
			{
				CompanyName = "Default Company",
				Address = "No Address Available",
				Phone = "N/A",
				Email = "N/A",
				Website = "",
				LogoUrl = ""
			};
		}

		return View(companyInfo);
	}
}

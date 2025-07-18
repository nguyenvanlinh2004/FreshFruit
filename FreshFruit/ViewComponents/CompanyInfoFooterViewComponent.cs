using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FreshFruit.Models;

namespace FreshFruit.ViewComponents
{
	public class CompanyInfoFooterViewComponent : ViewComponent
	{
		private readonly IServiceScopeFactory _scopeFactory;

		public CompanyInfoFooterViewComponent(IServiceScopeFactory scopeFactory)
		{
			_scopeFactory = scopeFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			using var scope = _scopeFactory.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<FreshFruitDbContext>();

			var info = await context.CompanyInfos.FirstOrDefaultAsync() ?? new CompanyInfo();
			return View(info);
		}
	}
}

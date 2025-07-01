using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FreshFruit.Services
{
	public class RazorViewToStringRenderer : IRazorViewToStringRenderer
	{
		private readonly IRazorViewEngine _viewEngine;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IServiceProvider _serviceProvider;
		private readonly IActionContextAccessor _actionContextAccessor;

		public RazorViewToStringRenderer(
			IRazorViewEngine viewEngine,
			ITempDataProvider tempDataProvider,
			IServiceProvider serviceProvider,
			IActionContextAccessor actionContextAccessor)
		{
			_viewEngine = viewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
			_actionContextAccessor = actionContextAccessor;
		}

		public async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model)
		{
			var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
			{
				Model = model
			};

			return await RenderViewToStringAsync(controller, viewName, model, viewData);
		}

		// ✅ Overload hỗ trợ truyền ViewDataDictionary
		public async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model, ViewDataDictionary viewData)
		{
			var actionContext = _actionContextAccessor.ActionContext
				?? throw new ArgumentNullException(nameof(_actionContextAccessor.ActionContext), "ActionContext is null");

			var viewResult = _viewEngine.FindView(actionContext, viewName, false);

			if (!viewResult.Success)
			{
				throw new InvalidOperationException($"Không tìm thấy view: {viewName}");
			}

			using var writer = new StringWriter();

			var viewContext = new ViewContext(
				actionContext,
				viewResult.View,
				viewData,
				new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
				writer,
				new HtmlHelperOptions()
			);

			await viewResult.View.RenderAsync(viewContext);
			return writer.ToString();
		}
	}
}

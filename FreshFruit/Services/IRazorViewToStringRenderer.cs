using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FreshFruit.Services
{
	public interface IRazorViewToStringRenderer
	{
		Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model);

		// ➕ Thêm overload hỗ trợ ViewData
		Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model, ViewDataDictionary viewData);
	}
}

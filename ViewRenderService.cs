using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Web
{
	public class ViewRenderService : IViewRenderService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IRazorViewEngine _razorViewEngine;
		private readonly IServiceProvider _serviceProvider;
		private readonly ITempDataProvider _tempDataProvider;

		public ViewRenderService(
			IHttpContextAccessor httpContextAccessor,
			IRazorViewEngine razorViewEngine,
			IServiceProvider serviceProvider,
			ITempDataProvider tempDataProvider)
		{
			_httpContextAccessor = httpContextAccessor;
			_razorViewEngine = razorViewEngine;
			_serviceProvider = serviceProvider;
			_tempDataProvider = tempDataProvider;
		}

		public async Task<string> RenderViewAsync<T>(string viewName, T model) where T : PageModel
		{
			var viewResult =
				_razorViewEngine.GetView("~/Pages/", $"{viewName}.cshtml",
					true);
			if (!viewResult.Success)
				throw new ArgumentException(
					$"{viewName} does not match any available view");


			var actionContext = new ActionContext(
				_httpContextAccessor.HttpContext ??
				new DefaultHttpContext { RequestServices = _serviceProvider },
				new RouteData(), new ActionDescriptor());
			await using var sw = new StringWriter();
			await viewResult.View.RenderAsync(new ViewContext(actionContext,
				viewResult.View,
				new ViewDataDictionary(new EmptyModelMetadataProvider(),
					new ModelStateDictionary()) { Model = model },
				new TempDataDictionary(actionContext.HttpContext,
					_tempDataProvider), sw, new HtmlHelperOptions()));
			return sw.ToString();
		}
	}
}

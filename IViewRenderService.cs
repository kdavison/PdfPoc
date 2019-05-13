using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web
{
	public interface IViewRenderService
	{
		Task<string> RenderViewAsync<T>(string viewName, T model) where T : PageModel;
	}
}

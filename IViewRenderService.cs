using System.Threading.Tasks;

namespace Web
{
	public interface IViewRenderService
	{
		Task<string> RenderViewAsync<T>(string viewName, T model);
	}
}

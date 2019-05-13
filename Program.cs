using System;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
	public class Program
	{
		public static Task Main(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.ConfigureServices(services =>
						{
							services
								.AddScoped<IViewRenderService, ViewRenderService>()
								.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
								.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()))
								.AddMvc()
								.SetCompatibilityVersion(CompatibilityVersion.Latest);
						})
						.Configure((env, app) =>
						{
							Console.WriteLine(env.HostingEnvironment.ContentRootPath);
							Console.WriteLine(env.HostingEnvironment.WebRootPath);
							if (env.HostingEnvironment.IsDevelopment())
								app.UseDeveloperExceptionPage();
							else
								app.UseExceptionHandler("/Error");

							app
								.UseStaticFiles()
								.UseRouting()
								.UseAuthentication()
								.UseAuthorization()
								.UseEndpoints(endpoints =>
								{
									endpoints.MapControllers();
									endpoints.MapRazorPages();
								});
						});
				})
				.Build()
				.RunAsync();
	}
}

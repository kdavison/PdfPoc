using System;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Extensions;
using SelectPdf;
using Web.Models;

namespace Web.Controllers
{
	[Route("/")]
	public class DefaultController : Controller
	{
		private readonly IConverter _converter;
		private readonly IViewRenderService _viewRenderService;

		private readonly QuoteResponse _quoteModel = new QuoteResponse
		{
			NamedInsured = "GWARbar",
			QuoteDate =
				SystemClock.Instance
					.InZone(DateTimeZoneProviders.Tzdb["America/Denver"])
					.GetCurrentDate(),
			PolicyEffectiveDate = SystemClock.Instance
				.InZone(DateTimeZoneProviders.Tzdb["America/Denver"])
				.GetCurrentDate().PlusDays(15)
		};

		public DefaultController(IConverter converter,
			IViewRenderService viewRenderService)
		{
			_converter = converter;
			_viewRenderService = viewRenderService;
		}

		[HttpGet("dink")]
		public async Task<FileContentResult> GetDinkAsync() =>
			File(
				_converter.Convert(new HtmlToPdfDocument
				{
					GlobalSettings
						=
						{
							ColorMode = ColorMode.Color,
							Orientation = Orientation.Portrait,
							PaperSize = PaperKind.A4
						},
					Objects =
					{
						new ObjectSettings
						{
							PagesCount = true,
							HtmlContent =
								await _viewRenderService
									.RenderViewAsync("Quote",
										_quoteModel),
							WebSettings =
							{
								DefaultEncoding = "utf-8"
							},
							HeaderSettings =
							{
								FontSize = 9,
								Right =
									"Page [page] of [toPage]",
								Line = true,
								Spacing = 2.812
							}
						}
					}
				}), "application/pdf", "PieQuoteDink.pdf");

		[HttpGet("select")]
		public async Task<FileContentResult> GetSelectAsync()
		{
			// instantiate a html to pdf converter object
			var converter = new HtmlToPdf
			{
				// Set options
				Options =
				{
					ColorSpace = PdfColorSpace.RGB,
					RenderingEngine = RenderingEngine.WebKit,
					PdfPageSize = PdfPageSize.A4,
					PdfPageOrientation = PdfPageOrientation.Portrait,
					MarginLeft = 10,
					MarginRight = 10,
					MarginTop = 20,
					MarginBottom = 20,
					CssMediaType = HtmlToPdfCssMediaType.Print
				}
			};

			// create a new pdf document converting an url
			var doc = converter.ConvertHtmlString(
				await _viewRenderService.RenderViewAsync("Quote", _quoteModel));
			var result = converter.ConversionResult;
			doc.DocumentInformation.Title = result.WebPageInformation.Title;
			doc.DocumentInformation.Subject =
				result.WebPageInformation.Description;
			doc.DocumentInformation.Keywords =
				result.WebPageInformation.Keywords;
			doc.DocumentInformation.Author = "Aaron Holderman";
			doc.DocumentInformation.CreationDate = DateTime.UtcNow;

			// save pdf document
			var pdf = doc.Save();

			// close pdf document
			doc.Close();

			return File(pdf, "application/pdf", "PieQuoteSelect.pdf");
		}
	}
}

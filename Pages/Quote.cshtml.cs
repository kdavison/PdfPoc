using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;

namespace Web.Pages
{
	public class QuoteModel : PageModel
	{
		public void OnGet()
		{
			NamedInsured = "Timber Creek Pallets";
			QuoteDate = SystemClock.Instance.GetCurrentInstant()
				.InZone(DateTimeZoneProviders.Tzdb["America/Denver"]).Date;
			PolicyEffectiveDate = QuoteDate.PlusDays(15);
		}

		public string NamedInsured { get; set; }

		public LocalDate PolicyEffectiveDate { get; set; }

		public LocalDate QuoteDate { get; set; }
	}
}

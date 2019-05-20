using System.Collections.Generic;
using NodaTime;
using NodaTime.Extensions;

namespace Web.Models
{
	public class QuoteResponse
	{
		public string NamedInsured { get; set; } = "Merchant's Saloon";
		public LocalDate PolicyEffectiveDate { get; set; } = new LocalDate(2019, 9, 11);

		public LocalDate QuoteDate { get; set; } = SystemClock.Instance
			.InZone(DateTimeZoneProviders.Tzdb["America/Denver"])
			.GetCurrentDate();

		public string InsuranceCompany { get; set; } = "Sirius America";

		public uint EstimatedAnnualPremium { get; set; } = 5450;

		public IEnumerable<ExposureResponse> Classes { get; set; } = new[]
		{
			new ExposureResponse
			{
				Class = "9079",
				State = "CA",
				Description = "RESTAURANTS OR TAVERNS - all employees - including musicians and entertainers",
				Payroll = 150000,
				Rate = 3.32m,
				PercentOfPayroll = 3.53m,
				Premium = 4973
			},
			new ExposureResponse
			{
				Class = "8810",
				State = "CA",
				Description = "CLERICAL OFFICE EMPLOYEES - N.O.C.",
				Payroll = 50000,
				Rate = 0.39m,
				PercentOfPayroll = 0.311m,
				Premium = 195
			}
		};

		public uint ClassPremium { get; set; } = 5119;

		public RateAmount Deductible { get; set; } =
			new RateAmount {Rate = 1, Amount = 0};

		public RateAmount CurrentEmod { get; set; } =
			new RateAmount {Rate = 1.01m, Amount = 77};

		public RateAmount ScheduleMod { get; set; }

		public RateAmount PremiumDiscount { get; set; }

		public uint ExpenseConstant { get; set; } = 160;

		public RateAmount Terrorism { get; set; }
		public RateAmount Catastrophe { get; set; }

		public uint TotalEstimatedPremium { get; set; }

		public IEnumerable<TaxResponse> Taxes { get; set; } = new[]
		{
			new TaxResponse { Description = "WCARF Assesment", Rate = 0.014479m, Amount = 77 },
			new TaxResponse { Description = "Fraud Surcharge", Rate = 0.014479m, Amount = 77 },
			new TaxResponse { Description = "WCARF Assesment", Rate = 0.014479m, Amount = 77 },
			new TaxResponse { Description = "WCARF Assesment", Rate = 0.014479m, Amount = 77 },
			new TaxResponse { Description = "WCARF Assesment", Rate = 0.014479m, Amount = 77 },
			new TaxResponse { Description = "WCARF Assesment", Rate = 0.014479m, Amount = 77 }
		};
	}
}

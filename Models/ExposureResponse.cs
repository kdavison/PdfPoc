
namespace Web.Models
{
	public class ExposureResponse
	{
		public string State { get; set; }
		public string Class { get; set; }
		public string Description { get; set; }
		public uint Payroll { get; set; }
		public decimal Rate { get; set; }
		public decimal PercentOfPayroll { get; set; }
		public uint Premium { get; set; }
	}
}

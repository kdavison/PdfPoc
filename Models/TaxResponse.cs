using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class TaxResponse
	{
		public string Description { get; set; }
		public decimal Rate { get; set; }
		public uint Amount { get; set; }
	}
}

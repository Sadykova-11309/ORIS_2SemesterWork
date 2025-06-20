using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Contracts.Contracts
{
	public class StationContract
	{
		public string Name { get; set; }
		public int Distance { get; set; }
		public string Type { get; set; }
		public int Price { get; set; }
		public string Slot { get; set; }
		public string Status { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Contracts.Contracts
{
	public class SessionContract
	{
		public Guid Station_id { get; set; }
		public string Number { get; set; }
		public string Date { get; set; }
		public string Client_name { get; set; }
		public string Location { get; set; }
		public string Info { get; set; }
		public int Energy { get; set; }
		public int Cost { get; set; }
		public string Status { get; set; }
	}
}

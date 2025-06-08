using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Contracts.Contracts
{
	public class RegisterContract
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public bool AcceptTerms { get; set; }
	}
}
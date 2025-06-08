using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Contracts.Contracts
{
	public class LoginContract
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
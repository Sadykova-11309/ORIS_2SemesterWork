using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Infrastucture.Extensions
{
	public static class IdentityExtensions
	{
		public static Guid GetUserId(this ClaimsPrincipal user)
		{
			var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
						  ?? throw new InvalidOperationException("NameIdentifier claim is missing");
			return Guid.Parse(idClaim.Value);
		}
	}
}



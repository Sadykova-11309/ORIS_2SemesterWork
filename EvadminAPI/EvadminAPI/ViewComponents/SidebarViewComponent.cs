using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvadminAPI.ViewComponents
{
	public class SidebarViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var role = UserClaimsPrincipal?.FindFirst(ClaimTypes.Role)?.Value;
			return View(role);
		}
	}
}

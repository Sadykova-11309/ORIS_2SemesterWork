using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class PagesController : Controller
	{
		[Authorize(Roles = "manager, owner")]
		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			return View("profile");
		}
	}
}

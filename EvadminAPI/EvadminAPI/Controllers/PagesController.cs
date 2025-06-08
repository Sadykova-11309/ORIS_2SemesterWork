using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class PagesController : Controller
	{
		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			return View("profile");
		}
	}
}

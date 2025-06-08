using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class HomeController : Controller
	{
		[HttpGet("Dashboard")]
		public async Task<IActionResult> Dashboard()
		{
			return View("index");
		}
	}
}

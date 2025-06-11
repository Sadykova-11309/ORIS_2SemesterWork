using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class ManagerController : Controller
	{
		[Authorize(Roles = "manager")]
		[HttpGet("station")]
		public async Task<IActionResult> GetStation() => View("station");

		[Authorize(Roles = "manager")]
		[HttpGet("management")]
		public async Task<IActionResult> GetManagement() => View("management");
	}
}

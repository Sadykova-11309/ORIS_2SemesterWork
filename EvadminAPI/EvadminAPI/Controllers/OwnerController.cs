using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class OwnerController : Controller
	{
		[Authorize(Roles = "owner")]
		[HttpGet("information")]
		public async Task<IActionResult> GetInformation() => View("information");
	}
}

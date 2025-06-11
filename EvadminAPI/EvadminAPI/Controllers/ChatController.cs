using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class ChatController : Controller
	{
		[Authorize(Roles = "manager, owner")]
		[HttpGet("Chat")]
		public async Task<IActionResult> Chat()
		{
			return View("chat");
		}
	}
}

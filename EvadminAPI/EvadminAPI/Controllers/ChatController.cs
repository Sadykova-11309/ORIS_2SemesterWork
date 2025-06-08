using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class ChatController : Controller
	{
		[HttpGet("Chat")]
		public async Task<IActionResult> Chat()
		{
			return View("chat");
		}
	}
}

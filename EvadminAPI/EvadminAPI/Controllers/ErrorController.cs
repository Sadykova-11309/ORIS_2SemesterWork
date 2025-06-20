using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[ApiExplorerSettings (IgnoreApi = true)]
	public class ErrorController : Controller
	{
		[Route("Error/404")]
		public async Task<IActionResult> NotFoundPage()
		{
			Response.StatusCode = StatusCodes.Status200OK;
			return View("404");
		}

		[Route("Error/500")]
		public async Task<IActionResult> ServerErrorPage()
		{
			Response.StatusCode = StatusCodes.Status200OK;
			return View("500");
		}
	}
}

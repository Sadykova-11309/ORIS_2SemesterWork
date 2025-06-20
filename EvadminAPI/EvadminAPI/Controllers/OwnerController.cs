using EvadminAPI.Infrastucture.Extensions;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	[Authorize(Roles = "owner")]
	public class OwnerController : Controller
	{
		private readonly IOwnerService _ownerService;

		public OwnerController(IOwnerService ownerService)
		{
			_ownerService = ownerService;
		}
				
		[HttpGet("information")]
		public async Task<IActionResult> GetInformation() => View("information");

		[HttpGet("metrics")]
		public async Task<IActionResult> GetOwnerMetrics()
		{
			Guid ownerId = User.GetUserId();
			var metrics = await _ownerService.GetOwnerMetricsAsync(ownerId);
			return Ok(metrics);
		}
	}
}

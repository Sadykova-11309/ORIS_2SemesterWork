using EvadminAPI.DataBase;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EvadminAPI.Services.Services.ManagerMetrics;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class ManagerController : Controller
	{
		private readonly IManagerService _managerService;

		public ManagerController(IManagerService managerService)
		{
			_managerService = managerService;
		}

		[Authorize(Roles = "manager")]
		[HttpGet("station")]
		public async Task<IActionResult> GetStation() => View("station");

		[Authorize(Roles = "manager")]
		[HttpGet("management")]
		public async Task<IActionResult> GetManagement() => View("management");

		[HttpGet("metrics")]
		public async Task<IActionResult> GetMetrics()
		{
			var metrics = await _managerService.GetMetricsAsync();
			return Ok(metrics);
		}
	}
}

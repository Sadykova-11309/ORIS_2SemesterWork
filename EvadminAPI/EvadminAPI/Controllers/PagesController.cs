using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class PagesController : Microsoft.AspNetCore.Mvc.Controller
	{
		private readonly UserService _userService;

		public PagesController(UserService userService)
		{
			_userService = userService;
		}

		[Authorize(Roles = "manager, owner")]
		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			return View("profile");
		}

		[Authorize(Roles = "manager, owner")]
		[HttpGet("current")]
		public async Task<IActionResult> GetCurrentUser()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				if (string.IsNullOrEmpty(userId))
					return Unauthorized();

				var user = await _userService.GetById(Guid.Parse(userId));

				if (user == null)
					return NotFound();

				return Ok(new
				{
					id = user.Id,
					fullName = user.FullName,
					email = user.Email,
					role = user.Role.Name,
					isActive = user.is_active
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

	}
}

using EvadminAPI.Contracts.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class AccountController : Controller
	{
		private readonly Services.Services.AuthenticationService _authenticationService;

		public AccountController(Services.Services.AuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpGet("login")]
		public async Task<IActionResult> GetLogin() => View("login");

		[Authorize(Roles = "manager")]
		[HttpGet("register")]
		public async Task<IActionResult> GetRegister() => View("registration_form");


		[Authorize(Roles = "manager")]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterContract user)
		{
			var result = await _authenticationService.Register(user);
			if (result != null)
			{
				return Ok(result);
			}

			return Ok("Регистрация успешна");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginContract user)
		{
			var result = await _authenticationService.Login(user);
			if (result != null)
			{
				return Ok(result);
			}

			return Ok(new { message = "Успешный вход" });
		}
	}

}
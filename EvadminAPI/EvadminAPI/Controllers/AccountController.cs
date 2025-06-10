using EvadminAPI.Contracts.Contracts;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class AccountController : Controller
	{
		private readonly UserService _userService;

		public AccountController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet("login")]
		public async Task<IActionResult> GetLogin() => View("login");

		[HttpGet("register")]
		public async Task<IActionResult> GetRegister() => View("registration_form");

		//[HttpGet("forgot-password")]
		//public async Task<IActionResult> GetForgot() => View("forgot");


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterContract user)
		{
			var email = _userService.GetByEmail(user.Email);
			if (email.Result != null)
			{
				return Ok("Вы уже зарегистрированы под этой почтой");
			}

			await _userService.Register(user);
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginContract user)
		{
			var result = await _userService.Login(user);
			if (result == null)
			{
				return Ok("Неверный пароль или вы не зарегистрированы");
			}

			Response.Cookies.Append("ass-token", result);

			return Ok(new { result });
		}

		//[HttpPost("forgot")]
		//public async Task<IActionResult> ForgotPassword([FromBody] UserModel user)
		//{
		//	var token = await _userService.GetByEmail(user.Email);
		//	if (token == null)
		//	{
		//		return Ok("Вы не зарегистрированы");
		//	}
		//	return Ok($"Ваш пароль {token.Password}");
		//}
	}
}
using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase.Models;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

		//[Authorize(Roles = "manager")]
		[HttpGet("register")]
		public async Task<IActionResult> GetRegister() => View("registration_form");

		//[Authorize(Roles = "manager")]
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
			var userModel = await _userService.GetByEmail(user.Email);
			if (userModel == null)
			{
				return Ok("Пользователь не найден");
			}

			var token = await _userService.Login(user);
			if (token == null)
			{
				return Ok("Неверный пароль или вы не зарегистрированы");
			}

			await Authenticate(userModel);

			return Ok(new { message = "Успешный вход" });

		}

		private async Task Authenticate(UserModel user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
			};

			// Добавляем роль, если она есть и не пустая
			if (!string.IsNullOrEmpty(user.Role?.Name))
			{
				claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name));
			}

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}
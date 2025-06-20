using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace EvadminAPI.Services.Services
{
	public class AuthenticationService
	{
		private readonly UserService _userService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenticationService(UserService userService, IHttpContextAccessor httpContextAccessor)
		{
			_userService = userService;
			_httpContextAccessor = httpContextAccessor;
		}


		public async Task<string> Register(RegisterContract user)
		{
			var email = await _userService.GetByEmail(user.Email);
			if (email != null)
			{
				return "Вы уже зарегистрированы под этой почтой";
			}

			await _userService.Register(user);
			return null; // Успех
		}

		public async Task<string> Login(LoginContract user)
		{
			var userModel = await _userService.GetByEmail(user.Email);
			if (userModel == null)
			{
				return "Пользователь не найден";
			}

			var token = await _userService.Login(user);
			if (token == null)
			{
				return "Неверный пароль или вы не зарегистрированы";
			}

			await Authenticate(userModel);
			return null; // Успех
		}

		private async Task Authenticate(UserModel user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),        
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
			};

			if (!string.IsNullOrEmpty(user.Role?.Name))
				claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name));

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}

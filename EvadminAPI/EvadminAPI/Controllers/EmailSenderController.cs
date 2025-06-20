using EvadminAPI.Contracts.Contracts;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class EmailSenderController : Controller
	{
		private readonly EmailService _emailService;
		private readonly ILogger<EmailSenderController> _logger;

		public EmailSenderController(EmailService emailService, ILogger<EmailSenderController> logger)
		{
			_emailService = emailService;
			_logger = logger;
		}

		[HttpPost("registration")]
		[AllowAnonymous]
		public async Task<IActionResult> SendRegistrationEmail([FromBody] RegisterContract user)
		{
			_logger.LogInformation("Получен запрос на отправку письма. Email: {Email}, FullName: {FullName}", 
				user?.Email ?? "null", user?.FullName ?? "null");

			if (user == null)
			{
				_logger.LogError("Получен null вместо данных пользователя");
				return BadRequest("Данные пользователя не предоставлены");
			}

			if (string.IsNullOrEmpty(user.Email))
			{
				_logger.LogError("Email не предоставлен");
				return BadRequest("Email обязателен");
			}

			try
			{
				_emailService.SendRegistrationEmail(user.FullName, user.Email, user.Password);
				_logger.LogInformation("Письмо успешно отправлено на {Email}", user.Email);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка при отправке письма на {Email}: {Message}", 
					user.Email, ex.Message);
				return StatusCode(500, ex.Message);
			}
		}
	}
}

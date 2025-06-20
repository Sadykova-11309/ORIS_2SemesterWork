using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Services.Services
{
	public class EmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void SendRegistrationEmail(string fullName, string email, string password)
		{
			var smtpSettings = _configuration.GetSection("SmtpSettings");
			var fromEmail = smtpSettings["FromEmail"];
			var fromPassword = smtpSettings["FromPassword"];
			var smtpServer = smtpSettings["SmtpServer"];
			var smtpPort = int.Parse(smtpSettings["SmtpPort"]);

			var subject = "Регистрация в сервисе Evadmin";
			var body = $"""
                Уважаемый {fullName}!
                Вы были зарегистрированы в нашем сервисе.
                Ваша почта для входа: {email}
                Ваш пароль: {password}
            """;

			SendEmail(fromEmail, fromPassword, smtpServer, smtpPort, email, subject, body);
		}

		private void SendEmail(string fromEmail, string fromPassword, string smtpServer,
			int smtpPort, string toEmail, string subject, string body)
		{
			using (var letter = new MailMessage(fromEmail, toEmail))
			{
				letter.Subject = subject;
				letter.Body = body;
				letter.IsBodyHtml = false;
				using (var client = new SmtpClient(smtpServer, smtpPort))
				{
					client.EnableSsl = true;
					client.Credentials = new NetworkCredential(fromEmail, fromPassword);
					client.Send(letter);
				}
			}
		}
	}
}

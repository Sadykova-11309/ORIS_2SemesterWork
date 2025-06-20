using EvadminAPI.DataBase.Models;
using EvadminAPI.Infrastucture;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EvadminAPI.AuthCheck
{
	public static class AuthChecker
	{
		public static void AddAuthOption(
		   this IServiceCollection services,
		   IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection(nameof(JwtOption)).Get<JwtOption>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
					};

					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							context.Token = context.Request.Cookies["ass-token"];
							return Task.CompletedTask;
						}
					};
				});
			services.AddAuthorization();
		}
	}
}
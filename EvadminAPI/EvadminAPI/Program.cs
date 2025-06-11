using EvadminAPI.AuthCheck;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Configurations;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repositories;
using EvadminAPI.DataBase.Repositories.Interfaces;
using EvadminAPI.Infrastucture;
using EvadminAPI.Services.Mapping;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EvadminAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddControllersWithViews();

			builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));

			builder.Services.AddDbContext<MyApplicationContext>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbContext")));

			builder.Services.AddScoped<UserService>();
			builder.Services.AddScoped<JwtOption>();
			builder.Services.AddScoped<JwtProvider>();
			builder.Services.AddScoped<PasswordHasher>();
			builder.Services.AddScoped<MyApplicationContext>();
			builder.Services.AddScoped<UserConfiguration>();
			builder.Services.AddScoped<UserModel>();
			builder.Services.AddScoped<IUserModelRepository, UserModelRepository>();

			builder.Services.AddAutoMapper(typeof(AutoMappingProducts));

			builder.Services.AddAuthOption(builder.Configuration);

			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});

			// ����������� ������������ ��������������
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
			.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			{
				options.LoginPath = "/Account/login";
				options.AccessDeniedPath = "/Account/accessdenied";
				options.Cookie.Name = "auth_cookie";
				options.ExpireTimeSpan = TimeSpan.FromHours(1);
				options.SlidingExpiration = true;

				// ���������� ������ ��������� ��� ����������
				options.Cookie.SecurePolicy = CookieSecurePolicy.None; // ��������� HTTP
				options.Cookie.SameSite = SameSiteMode.Lax;           // ��������� cross-site
				options.Cookie.HttpOnly = true;                       // ������ �� XSS

				// �������������� ��������� ��� �������
				options.Events = new CookieAuthenticationEvents
				{
					OnRedirectToLogin = context =>
					{
						context.Response.StatusCode = 401;
						return Task.CompletedTask;
					},
					OnRedirectToAccessDenied = context =>
					{
						context.Response.StatusCode = 403;
						return Task.CompletedTask;
					}
				};
			});

			//// ��������� �������������� ����� ����
			//builder.Services.AddAuthentication(options =>
			//{
			//	// ������������� ����� �������������� �� ��������� �� ����
			//	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//})
			//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			//{
			//	options.LoginPath = "/Account/login"; // ���� ��� ��������� ��� ���������������� �������
			//	options.AccessDeniedPath = "/Account/accessdenied"; // ���� ��� ������ � �������
			//	options.Cookie.Name = "auth_cookie"; // ��� ���� (����� ��������)
			//	options.ExpireTimeSpan = TimeSpan.FromHours(1); // ����� ����� ����
			//	options.SlidingExpiration = true; // ��������� ����� ����� ���� ��� ����������
			//});

			var app = builder.Build();

			app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Lax
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
using EvadminAPI.AuthCheck;
using EvadminAPI.Contracts.Abstractions;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Configurations;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repositories;
using EvadminAPI.DataBase.Repositories.Interfaces;
using EvadminAPI.DataBase.Repository;
using EvadminAPI.DataBase.Repository.Extensions;
using EvadminAPI.Infrastucture;
using EvadminAPI.Infrastucture.Template;
using EvadminAPI.Middlewares;
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

			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllers()
				.AddJsonOptions(o =>
				{
					o.JsonSerializerOptions.ReferenceHandler =
					System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
					o.JsonSerializerOptions.DefaultIgnoreCondition =
					System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
				});
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddControllersWithViews();

			builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));

			builder.Services.AddDbContext<MyApplicationContext>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbContext")));

			builder.Services.AddScoped<UserService>();
			builder.Services.AddScoped<AuthenticationService>();
			builder.Services.AddScoped<IStationService, StationService>();
			builder.Services.AddScoped<ISessionService, SessionService>();
			builder.Services.AddScoped<IManagerService, ManagerService>();
			builder.Services.AddScoped<IOwnerService, OwnerService>();
			builder.Services.AddScoped<EmailService>();
			builder.Services.AddScoped<JwtOption>();
			builder.Services.AddScoped<JwtProvider>();
			builder.Services.AddScoped<PasswordHasher>();
			builder.Services.AddScoped<MyApplicationContext>();
			builder.Services.AddScoped<UserConfiguration>();
			builder.Services.AddScoped<StationConfiguration>();
			builder.Services.AddScoped<SessionConfiguration>();
			builder.Services.AddScoped<UserModel>();
			builder.Services.AddScoped<ChargingStationModel>();
			builder.Services.AddScoped<ChargingSessionModel>();
			builder.Services.AddScoped<IChargingStationModelRepository, ChargingStationModelRepository>();
			builder.Services.AddScoped<IChargingSessionModelRepository, ChargingSessionModelRepository>();
			builder.Services.AddScoped<IUserModelRepository, UserModelRepository>();
			builder.Services.AddScoped<EmailRepository>();

			builder.Services.AddScoped<ITemplateEngine, ScribanTemplateEngine>();


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
						if (context.Request.Path.StartsWithSegments("/api") || context.Request.Path.StartsWithSegments("/swagger"))
						{
							context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						}
						else
						{
							context.Response.Redirect("/Account/login");
						}
						return Task.CompletedTask;
					}
				};
			});

			

			var app = builder.Build();

			app.MapControllerRoute(
				name: "error",
				pattern: "Error/{action}",
				defaults: new { controller = "Error" });

			app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseMiddleware<ErrorHandlingMiddlware>();

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Error"), appBuilder =>
			{
				app.UseAuthentication();
				app.UseAuthorization();
			});

			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.Lax
			});

			app.MapControllers();

			app.Run();
		}
	}
}
using EvadminAPI.AuthCheck;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Configurations;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repositories;
using EvadminAPI.DataBase.Repositories.Interfaces;
using EvadminAPI.Infrastucture;
using EvadminAPI.Services.Mapping;
using EvadminAPI.Services.Services;
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

			var app = builder.Build();

			app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();

			app.MapControllers();

			app.Run();
		}
    }
}

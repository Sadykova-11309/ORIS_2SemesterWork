using EvadminAPI.DataBase.Configurations;
using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EvadminAPI.DataBase
{
	public class MyApplicationContext : DbContext
	{

		public DbSet<UserModel> Users { get; set; }
		public DbSet<Role> Roles { get; set; }


		public MyApplicationContext(DbContextOptions<MyApplicationContext> dbContext) : base(dbContext) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());

			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, Name = "User" },
				new Role { Id = 2, Name = "Admin" }
			);

			base.OnModelCreating(modelBuilder);
		}

	}
}
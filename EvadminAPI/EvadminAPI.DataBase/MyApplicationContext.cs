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


		public MyApplicationContext(DbContextOptions<MyApplicationContext> dbContext) : base(dbContext) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());

			base.OnModelCreating(modelBuilder);
		}

	}
}
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
		public DbSet<ChargingStationModel> Stations	{ get; set; }
		public DbSet<ChargingSessionModel> Sessions { get; set; }


		public MyApplicationContext(DbContextOptions<MyApplicationContext> dbContext) : base(dbContext) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new StationConfiguration());
			modelBuilder.ApplyConfiguration(new SessionConfiguration());

			modelBuilder.Entity<Role>()
				.HasData(
				new Role { Id = 1, Name = "owner" },
				new Role { Id = 2, Name = "manager" }
			);

			modelBuilder.Entity<ChargingStationModel>()
			.HasOne(p => p.User)
			.WithMany(t => t.Stations)
			.HasForeignKey(p => p.Owner_id)
			.HasPrincipalKey(t => t.Id);

			modelBuilder.Entity<ChargingSessionModel>()
			.HasOne(p => p.Station)
			.WithMany(t => t.Sessions)
			.HasForeignKey(p => p.Station_id)
			.HasPrincipalKey(t => t.Id);

			base.OnModelCreating(modelBuilder);
		}

	}
}
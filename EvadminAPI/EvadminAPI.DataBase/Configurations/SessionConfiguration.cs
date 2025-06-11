using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Configurations
{
	public class SessionConfiguration : IEntityTypeConfiguration<ChargingSessionModel>
	{
		public void Configure(EntityTypeBuilder<ChargingSessionModel> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}

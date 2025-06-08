using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvadminAPI.DataBase.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserModel>
	{
		public void Configure(EntityTypeBuilder<UserModel> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}

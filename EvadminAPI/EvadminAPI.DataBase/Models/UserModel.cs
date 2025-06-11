using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Models
{
	public class UserModel
	{
		public Guid Id { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public int RoleId { get; set; } 

		public Role Role { get; set; } = null!;

		public bool is_active { get; set; } = true;

		public List<ChargingStationModel>? Stations { get; set; }


		public static UserModel CreateModel(UserModel model, string password)
		{
			return new UserModel()
			{
				Id = Guid.NewGuid(),
				FullName = model.FullName,
				Email = model.Email,
				Password = password,
				RoleId = model.RoleId,
				is_active = model.is_active,
				Stations = model.Stations
			};
		}
	}

}
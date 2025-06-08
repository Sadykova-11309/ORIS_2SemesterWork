using System;
using System.Collections.Generic;
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

		public static UserModel CreateModel(UserModel model, string password)
		{
			return new UserModel()
			{
				Id = Guid.NewGuid(),
				FullName = model.FullName,
				Email = model.Email,
				Password = password,
			};
		}
	}
}
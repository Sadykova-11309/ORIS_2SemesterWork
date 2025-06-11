using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Models
{
	public class ChargingStationModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Distance { get; set; }
		public string Type { get; set; }
		public int Price { get; set; }
		public string Slot { get; set; }
		public string Status { get; set; }
		public List<ChargingSessionModel> Sessions { get; set; }
		public Guid Owner_id { get; set; }

		[ForeignKey("Owner_id")]
		public UserModel User { get; set; }

		public static ChargingStationModel CreateModel(ChargingStationModel model)
		{
			return new ChargingStationModel()
			{
				Id = Guid.NewGuid(),
				Name = model.Name,
				Distance = model.Distance,
				Type = model.Type,
				Price = model.Price,
				Slot = model.Slot,
				Status = model.Status,
				Sessions = model.Sessions,
				Owner_id = model.Owner_id,
				User = model.User
			};
		}
	}
}


//`status` (`available` | `unavailable` | `in_use`),

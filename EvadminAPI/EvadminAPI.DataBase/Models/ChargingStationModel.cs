using System.ComponentModel.DataAnnotations.Schema;


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
		public string Status { get; set; } = "Active";
		public List<ChargingSessionModel> Sessions { get; set; }

		[ForeignKey("User")]
		public Guid Owner_id { get; set; } 
		public UserModel User { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

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
				User = model.User,
				Latitude = model.Latitude,
				Longitude = model.Longitude,
				
			};
		}
	}
}



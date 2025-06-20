using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Models
{
	public class ChargingSessionModel
	{
		public Guid Id { get; set; }
		public string Number { get; set; }
		public string Date { get; set; }
		public string Client_name { get; set; }
		public string Location { get; set; }
		public string Info { get; set; }
		public int Energy { get; set; }
		public int Cost { get; set; }
		public string Status { get; set; }

		[ForeignKey("Station")]
		public Guid Station_id { get; set; }
		public ChargingStationModel Station	{ get; set; }

		public static ChargingSessionModel CreateModel(ChargingSessionModel model)
		{
			return new ChargingSessionModel()
			{
				Id = Guid.NewGuid(),
				Number = model.Number,
				Date = model.Date,
				Client_name = model.Client_name,
				Location = model.Location,
				Info = model.Info,
				Cost = model.Cost,
				Status = model.Status,
				Station_id = model.Station_id,
				Station = model.Station,
			};
		}
	}
}
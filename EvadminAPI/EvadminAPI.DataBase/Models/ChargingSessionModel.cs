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
		public string Client_name { get; set; }
		public string Start_time { get; set; }
		public string End_time { get; set; }
		public int Energy_consumed { get; set; }
		public int Cost { get; set; }
		public string Status { get; set; }
		public Guid Station_id { get; set; }

		[ForeignKey("Station_id")]
		public ChargingStationModel Station	{ get; set; }

		public static ChargingSessionModel CreateModel(ChargingSessionModel model)
		{
			return new ChargingSessionModel()
			{
				Id = Guid.NewGuid(),
				Client_name = model.Client_name,
				Start_time = model.Start_time,
				End_time = model.End_time,
				Energy_consumed = model.Energy_consumed,
				Cost = model.Cost,
				Status = model.Status,
				Station_id = model.Station_id,
				Station = model.Station,
			};
		}
	}
}


//`id`, `station_id` (FK), `client_name`, `start_time` (дата/время начала), `end_time` (дата/время окончания), `energy_consumed` (кВт),
//	`cost` (рассчитывается: `energy_consumed* station.price`), `status` (`active` | `completed`).
using EvadminAPI.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Repository.Extensions
{
	public interface IChargingStationModelRepository
	{
		public Task<List<ChargingStationModel>> GetAll();

		public Task<ChargingStationModel> GetById(Guid id);

		public Task Create(ChargingStationModel station);

		public Task Delete(Guid id);

		public Task Update(ChargingStationModel station);
		public Task<bool> UpdateStatus(Guid Id);
	}
}

using EvadminAPI.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Repository.Extensions
{
	internal interface IChargingSessionModelRepository
	{
		public Task<List<ChargingSessionModel>> GetAll();

		public Task<ChargingSessionModel> GetById(Guid id);

		public Task Create(ChargingSessionModel session);

		public Task Delete(Guid id);

		public Task Update(ChargingSessionModel session);
	}
}

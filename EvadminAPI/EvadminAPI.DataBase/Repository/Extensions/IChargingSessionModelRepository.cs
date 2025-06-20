using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Repository.Extensions
{
	public interface IChargingSessionModelRepository
	{
		public Task<List<ChargingSessionModel>> GetAll();

		public Task Create(ChargingSessionModel session);

		public Task Delete(Guid id);

		public Task Update(ChargingSessionModel session);
		public Task<ChargingSessionModel> GetById(Guid id);

		public Task<List<ChargingSessionModel>> GetByStationOwnerId(Guid ownerId);

		public Task<bool> UpdateStatus(Guid sessionId);
	}
}

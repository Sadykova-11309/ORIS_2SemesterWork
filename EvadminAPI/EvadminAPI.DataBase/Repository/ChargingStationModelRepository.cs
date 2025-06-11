using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.DataBase.Repository
{
	internal class ChargingStationModelRepository : IChargingStationModelRepository
	{
		private readonly MyApplicationContext _context;

		public ChargingStationModelRepository(MyApplicationContext context)
		{
			_context = context;
		}
		public async Task<List<ChargingStationModel>> GetAll()
		{
			var result = await _context.Stations
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<ChargingStationModel> GetById(Guid id)
		{
			var result = await _context.Stations
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task Update(ChargingStationModel station)
		{
			throw new NotImplementedException();
		}
		public async Task Create(ChargingStationModel station)
		{
			var result = ChargingStationModel.CreateModel(station);

			await _context.AddAsync(result);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			await _context.Users
				.AsNoTracking()
				.Where(x => x.Id == id)
				.ExecuteDeleteAsync();
		}
	}
}

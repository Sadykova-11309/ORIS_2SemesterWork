using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EvadminAPI.DataBase.Repository
{
	public class ChargingStationModelRepository : IChargingStationModelRepository
	{
		private readonly MyApplicationContext _context;

		public ChargingStationModelRepository(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task<List<ChargingStationModel>> GetAll()
		{
			return await _context.Stations
				.AsNoTracking()
				.Include(s => s.User)
				.ToListAsync();
		}

		public async Task<ChargingStationModel> GetById(Guid id)
		{
			return await _context.Stations
				.AsNoTracking()
				.Include(s => s.User)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task Update(ChargingStationModel station)
		{
			var existing = await _context.Stations.FindAsync(station.Id);
			if (existing == null) throw new KeyNotFoundException("Station not found");

			existing.Name = station.Name;
			existing.Distance = station.Distance;
			existing.Type = station.Type;
			existing.Price = station.Price;
			existing.Slot = station.Slot;
			existing.Status = station.Status;

			_context.Stations.Update(existing);
			await _context.SaveChangesAsync();
		}

		public async Task Create(ChargingStationModel station)
		{
			var ownerExists = await _context.Users.AnyAsync(u => u.Id == station.Owner_id);
			if (!ownerExists) throw new ArgumentException("Owner not found");

			await _context.Stations.AddAsync(station);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			await _context.Stations
				.Where(x => x.Id == id)
				.ExecuteDeleteAsync();
		}

		public async Task<bool> UpdateStatus(Guid id)
		{
			// Получаем текущий статус
			var currentStatus = await _context.Stations
				.Where(s => s.Id == id)
				.Select(s => s.Status)
				.FirstOrDefaultAsync();

			// Определяем новый статус
			var newStatus = currentStatus == "available"
				? "unavailable"
				: "available";

			// Выполняем обновление
			var rowsAffected = await _context.Stations
				.Where(s => s.Id == id)
				.ExecuteUpdateAsync(setters => setters
					.SetProperty(s => s.Status, newStatus));

			return rowsAffected > 0;
		}

		//private readonly MyApplicationContext _context;

		//public ChargingStationModelRepository(MyApplicationContext context)
		//{
		//	_context = context;
		//}
		//public async Task<List<ChargingStationModel>> GetAll()
		//{
		//	var result = await _context.Stations
		//		.AsNoTracking()
		//		.ToListAsync();

		//	return result;
		//}

		//public async Task<ChargingStationModel> GetById(Guid id)
		//{
		//	var result = await _context.Stations
		//		.AsNoTracking()
		//		.FirstOrDefaultAsync(x => x.Id == id);

		//	return result;
		//}

		//public async Task Update(ChargingStationModel station)
		//{
		//	throw new NotImplementedException();
		//}
		//public async Task Create(ChargingStationModel station)
		//{
		//	var result = ChargingStationModel.CreateModel(station);

		//	await _context.AddAsync(result);
		//	await _context.SaveChangesAsync();
		//}

		//public async Task Delete(Guid id)
		//{
		//	await _context.Users
		//		.AsNoTracking()
		//		.Where(x => x.Id == id)
		//		.ExecuteDeleteAsync();
		//}
	}
}

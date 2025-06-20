using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using Microsoft.EntityFrameworkCore;


namespace EvadminAPI.DataBase.Repository
{
	public class ChargingSessionModelRepository : IChargingSessionModelRepository
	{
		private readonly MyApplicationContext _context;

		public ChargingSessionModelRepository(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task<List<ChargingSessionModel>> GetAll()
		{
			return await _context.Sessions
				.AsNoTracking()
				.Include(s => s.Station)
				.ToListAsync();
		}

		public async Task Update(ChargingSessionModel session)
		{
			var existing = await _context.Sessions.FindAsync(session.Id);
			if (existing == null) throw new KeyNotFoundException("Session not found");

			existing.Number = session.Number;
			existing.Date = session.Date;
			existing.Client_name = session.Client_name;
			existing.Location = session.Location;
			existing.Info = session.Info;
			existing.Energy = session.Energy;
			existing.Cost = session.Cost;
			existing.Status = session.Status;

			_context.Sessions.Update(existing);
			await _context.SaveChangesAsync();
		}

		public async Task Create(ChargingSessionModel session)
		{
			await _context.Sessions.AddAsync(session);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			await _context.Sessions
				.Where(x => x.Id == id)
				.ExecuteDeleteAsync();
		}

		public async Task<ChargingSessionModel> GetById(Guid id)
		{
			return await _context.Sessions
			  .AsNoTracking()
			  .Include(s => s.Station)
			  .FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<List<ChargingSessionModel>> GetByStationOwnerId(Guid ownerId)
		{
			return await _context.Sessions
			  .Include(s => s.Station)
			  .Where(s => s.Station.Owner_id == ownerId)
			  .AsNoTracking()
			  .ToListAsync();
		}

		public async Task<bool> UpdateStatus(Guid sessionId)
		{
			var currentStatus = await _context.Sessions
				.Where(s => s.Id == sessionId)
				.Select(s => s.Status)
				.FirstOrDefaultAsync();

			var newStatus = currentStatus == "active"
				? "complete"
				: "active";

			var rowsAffected = await _context.Sessions
				.Where(s => s.Id == sessionId)
				.ExecuteUpdateAsync(setters => setters
					.SetProperty(s => s.Status, newStatus));

			return rowsAffected > 0;
		}
	}
}

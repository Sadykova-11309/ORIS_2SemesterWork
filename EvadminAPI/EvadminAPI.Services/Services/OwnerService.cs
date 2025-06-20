using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;


namespace EvadminAPI.Services.Services
{
	public interface IOwnerService
	{
		Task<OwnerMetrics> GetOwnerMetricsAsync(Guid ownerId);
	}
	public class OwnerService : IOwnerService
	{
		private readonly MyApplicationContext _context;

		public OwnerService(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task<OwnerMetrics> GetOwnerMetricsAsync(Guid ownerId)
		{
			var metrics = new OwnerMetrics();

			metrics.TotalStations = await _context.Stations
				.CountAsync(s => s.Owner_id == ownerId);

			metrics.ActiveSessions = await _context.Sessions
				.CountAsync(s => s.Station != null &&
						  s.Station.Owner_id == ownerId &&
						  s.Status == "active");

			metrics.InactiveSessions = metrics.TotalStations - metrics.ActiveSessions;

			metrics.TotalEnergy = await _context.Sessions
				.Where(s => s.Station != null &&
					   s.Station.Owner_id == ownerId) 
				.SumAsync(s => s.Energy);

			metrics.TotalRevenue = await _context.Sessions
				.Where(s => s.Station != null &&
					   s.Station.Owner_id == ownerId) 
				.SumAsync(s => s.Cost);

			return metrics;
		}

		

	}

	public class OwnerMetrics
	{
		public int TotalStations { get; set; }
		public int ActiveSessions { get; set; }
		public int InactiveSessions { get; set; }
		public decimal TotalEnergy { get; set; }
		public decimal TotalRevenue { get; set; }
	}

}

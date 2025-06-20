using EvadminAPI.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EvadminAPI.Services.Services
{
	public interface IManagerService
	{
		Task<ManagerMetrics> GetMetricsAsync();
	}

	public class ManagerService : IManagerService
	{
		private readonly MyApplicationContext _context;

		public ManagerService(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task<int> CountStationsAsync() =>
		await _context.Stations.CountAsync();

		public async Task<int> CountActiveStationsAsync() =>
			await _context.Sessions
				.CountAsync(s => s.Status == "active");

		public async Task<int> CountSessionsAsync() =>
			await _context.Sessions.CountAsync();

		public async Task<int> CountOwnersAsync() =>
			await _context.Users
				.CountAsync(u => u.RoleId == 1);
		public async Task<int> CountAvailableAsync() =>
			await _context.Stations
				.CountAsync(s => s.Status == "available");

		public async Task<int> CountUnavailableAsync() =>
			await _context.Stations
				.CountAsync(s => s.Status == "unavailable");

		// Методы для SumAsync
		public async Task<decimal> SumSessionEnergyAsync() =>
			await _context.Sessions.SumAsync(s => s.Energy);

		public async Task<decimal> SumSessionCostAsync() =>
			await _context.Sessions.SumAsync(s => s.Cost);

		// Полный метод для получения метрик
		public async Task<ManagerMetrics> GetMetricsAsync()
		{
			return new ManagerMetrics
			{
				TotalStations = await CountStationsAsync(),
				ActiveStations = await CountActiveStationsAsync(),
				TotalSessions = await CountSessionsAsync(),
				TotalEnergy = await SumSessionEnergyAsync(),
				TotalRevenue = await SumSessionCostAsync(),
				NewUsers = await CountOwnersAsync(),
				AvailableStations = await CountAvailableAsync(),
				UnavailableStations = await CountUnavailableAsync(),
			};
		}
	}

	public class ManagerMetrics
	{
		public int TotalStations { get; set; }
		public int ActiveStations { get; set; }
		public int TotalSessions { get; set; }
		public decimal TotalEnergy { get; set; }
		public decimal TotalRevenue { get; set; }
		public int NewUsers { get; set; }
		public int AvailableStations { get; set; }
		public int UnavailableStations { get; set; }

	}
}


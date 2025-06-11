using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace EvadminAPI.DataBase.Repository
{
	public class ChargingSessionModelRepository : IChargingSessionModelRepository
	{
		private readonly MyApplicationContext _context;

		public ChargingSessionModelRepository(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task Create(ChargingSessionModel session)
		{
			var result = ChargingSessionModel.CreateModel(session);

			await _context.AddAsync(result);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ChargingSessionModel>> GetAll()
		{
			var result = await _context.Sessions
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<ChargingSessionModel> GetById(Guid id)
		{
			var result = await _context.Sessions
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task Update(ChargingSessionModel session)
		{
			throw new NotImplementedException();
		}
	}
}

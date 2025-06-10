using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvadminAPI.DataBase.Repositories
{
	public class UserModelRepository : IUserModelRepository
	{
		private readonly MyApplicationContext _context;

		public UserModelRepository(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task Create(UserModel user, string password)
		{
			var result = UserModel.CreateModel(user, password);

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

		public async Task<List<UserModel>> GetAll()
		{
			var result = await _context.Users
				.Include(u => u.Role)
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<UserModel> GetById(Guid id)
		{
			var result = await _context.Users
				.Include(u => u.Role)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task<UserModel> GetByEmail(string email)
		{
			var result = await _context.Users
				 .Include(u => u.Role)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Email == email);

			return result;
		}

		public Task Update(UserModel user)
		{
			throw new NotImplementedException();
		}
	}
}
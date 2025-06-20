using EvadminAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace EvadminAPI.DataBase.Repository
{
	public class EmailRepository
	{
		private readonly MyApplicationContext _context;

		public EmailRepository(MyApplicationContext context)
		{
			_context = context;
		}

		public async Task<List<UserModel>> GetUser()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task CreateEmail(UserModel user)
		{
			var userEntity = new UserModel
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email
			};
			await _context.AddAsync(userEntity);
			await _context.SaveChangesAsync();
		}

	}
}

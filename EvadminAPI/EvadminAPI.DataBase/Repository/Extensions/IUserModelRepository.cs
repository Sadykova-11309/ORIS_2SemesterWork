using EvadminAPI.DataBase.Models;

namespace EvadminAPI.DataBase.Repositories.Interfaces
{
	public interface IUserModelRepository
	{
		public Task<List<UserModel>> GetAll();

		public Task<UserModel> GetById(Guid id);

		public Task<UserModel> GetByEmail(string email);

		public Task Create(UserModel user, string password);

		public Task Delete(Guid id);

		public Task Update(UserModel user);
	}
}
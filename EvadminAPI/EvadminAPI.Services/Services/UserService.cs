using AutoMapper;
using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repositories.Interfaces;
using EvadminAPI.Infrastucture;

namespace EvadminAPI.Services.Services
{
	public class UserService
	{
		private readonly IUserModelRepository _userModelRepository;
		private readonly JwtProvider _jwtProvider;
		private readonly PasswordHasher _passwordHasher;
		private readonly IMapper _mapper;

		public UserService(
			IUserModelRepository repository,
			JwtProvider jwtProvider,
			PasswordHasher passwordHasher,
			 IMapper mapper)
		{
			_userModelRepository = repository;
			_jwtProvider = jwtProvider;
			_passwordHasher = passwordHasher;
			_mapper = mapper;
		}

		public async Task Register(RegisterContract regitster)
		{

			var user = _mapper.Map<UserModel>(regitster);

			var password = _passwordHasher.GenerateTokenSHA(user.Password);

			await _userModelRepository.Create(user, password);
		}

		public async Task<string> Login(LoginContract login)
		{
			var user = await _userModelRepository.GetByEmail(login.Email);

			var result = _passwordHasher.Verify(login.Password, user.Password);

			if (!result) throw new Exception("Неверный пароль");

			if (user == null) throw new Exception("Такого пользователя с почтой не существует");

			var token = _jwtProvider.GenerateToken(user);

			return token;
		}

		public async Task Update(UserModel user)
		{
			await _userModelRepository.Update(user);
		}

		public async Task Remove(Guid id)
		{
			await _userModelRepository.Delete(id);
		}

		public async Task<List<UserModel>> GetAll()
		{
			return await _userModelRepository.GetAll();
		}

		public async Task<UserModel> GetById(Guid id)
		{
			return await _userModelRepository.GetById(id);
		}

		public async Task<UserModel> GetByEmail(string email)
		{
			return await _userModelRepository.GetByEmail(email);
		}
	}
}
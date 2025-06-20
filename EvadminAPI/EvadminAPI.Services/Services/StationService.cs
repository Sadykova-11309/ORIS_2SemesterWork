using AutoMapper;
using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using EvadminAPI.Infrastucture.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EvadminAPI.Services.Services
{
	public interface IStationService
	{
		Task<ChargingStationModel> CreateAsync(Guid ownerId, StationContract contract);
		Task<ChargingStationModel> UpdateAsync(Guid ownerId, Guid stationId, StationContract contract);
		Task DeleteAsync(Guid ownerId, Guid stationId);
		Task UpdateStatusAsync(Guid sessionId);
		Task<ChargingStationModel?> GetByIdAsync(ClaimsPrincipal user, Guid stationId);
		Task<List<ChargingStationModel>> GetAllAsync(ClaimsPrincipal user);
	}

	public class StationService : IStationService
	{
		private readonly MyApplicationContext _context;
		private readonly IChargingStationModelRepository _repository;
		private readonly IMapper _mapper;

		public StationService(IChargingStationModelRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<ChargingStationModel> CreateAsync(Guid ownerId, StationContract contract)
		{
			var model = _mapper.Map<ChargingStationModel>(contract);
			model.Id = Guid.NewGuid();
			model.Owner_id = ownerId;

			await _repository.Create(model);
			return model;
		}

		public async Task<ChargingStationModel> UpdateAsync(Guid ownerId, Guid stationId, StationContract contract)
		{
			var station = await _repository.GetById(stationId) ?? throw new KeyNotFoundException("Station not found");
			if (station.Owner_id != ownerId)
				throw new UnauthorizedAccessException("You don't own this station");

			station.Name = contract.Name;
			station.Distance = contract.Distance;
			station.Type = contract.Type;
			station.Price = contract.Price;
			station.Slot = contract.Slot;

			await _repository.Update(station);
			return station;
		}

		public async Task DeleteAsync(Guid ownerId, Guid stationId)
		{
			var station = await _repository.GetById(stationId) ?? throw new KeyNotFoundException("Station not found");
			if (station.Owner_id != ownerId)
				throw new UnauthorizedAccessException("You don't own this station");

			await _repository.Delete(stationId);
		}

		public async Task<ChargingStationModel?> GetByIdAsync(ClaimsPrincipal user, Guid stationId)
		{
			var station = await _repository.GetById(stationId);
			if (station == null) return null;

			// manager can view any station; owner can view only own stations
			if (user.IsInRole("manager") || station.Owner_id == user.GetUserId())
				return station;

			throw new UnauthorizedAccessException("Forbidden");
		}

		public async Task<List<ChargingStationModel>> GetAllAsync(ClaimsPrincipal user)
		{
			var stations = await _repository.GetAll();
			// Для менеджера возвращаем все станции
			if (user.IsInRole("manager"))
			{
				return stations;
			}
			// Для обычного пользователя фильтруем по владельцу
			var userId = user.GetUserId();
			return stations.Where(s => s.Owner_id == userId).ToList();
		}

		public async Task UpdateStatusAsync(Guid stationId)
		{
			var newStatus = await _repository.UpdateStatus(stationId);
			if (!newStatus) throw new KeyNotFoundException("Session not found");
		}
	}

}

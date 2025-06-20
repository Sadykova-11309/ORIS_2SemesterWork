using AutoMapper;
using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using EvadminAPI.Infrastucture.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Services.Services
{
	public interface ISessionService
	{
		Task<ChargingSessionModel> CreateAsync(Guid managerId, SessionContract contract);
		Task UpdateStatusAsync(Guid sessionId);
		Task<List<ChargingSessionModel>> GetAllAsync(ClaimsPrincipal user);
		Task<ChargingSessionModel?> GetByIdAsync(ClaimsPrincipal user, Guid sessionId);
		Task DeleteAsync(Guid sessionId);
	}

	public class SessionService : ISessionService
	{
		private readonly IChargingSessionModelRepository _sessionRepository;
		private readonly IChargingStationModelRepository _stationRepository;
		private readonly IMapper _mapper;

		public SessionService(
		IChargingSessionModelRepository sessionRepository,
		  IChargingStationModelRepository stationRepository,
		  IMapper mapper)
		{
			_sessionRepository = sessionRepository;
			_stationRepository = stationRepository;
			_mapper = mapper;
		}

		public async Task<ChargingSessionModel> CreateAsync(Guid managerId, SessionContract contract)
		{
			// Проверка существования станции
			var station = await _stationRepository.GetById(contract.Station_id);
			if (station == null) throw new ArgumentException("Station not found");

			var session = _mapper.Map<ChargingSessionModel>(contract);
			session.Id = Guid.NewGuid();

			await _sessionRepository.Create(session);
			return session;
		}

		public async Task UpdateStatusAsync(Guid sessionId)
		{
			
			var updated = await _sessionRepository.UpdateStatus(sessionId);
			if (!updated) throw new KeyNotFoundException("Session not found");
		}

		public async Task<List<ChargingSessionModel>> GetAllAsync(ClaimsPrincipal user)
		{
			if (user.IsInRole("manager"))
			{
				return await _sessionRepository.GetAll();
			}
			else
			{
				var ownerId = user.GetUserId();
				return await _sessionRepository.GetByStationOwnerId(ownerId);
			}
		}

		public async Task<ChargingSessionModel?> GetByIdAsync(ClaimsPrincipal user, Guid sessionId)
		{
			var session = await _sessionRepository.GetById(sessionId);
			if (session == null) return null;

			// Проверка прав доступа
			if (user.IsInRole("manager")) return session;

			if (user.IsInRole("owner") && session.Station.Owner_id == user.GetUserId())
				return session;

			throw new UnauthorizedAccessException("Forbidden");
		}

		public async Task DeleteAsync(Guid sessionId)
		{
			await _sessionRepository.Delete(sessionId);
		}

	}
}


using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase;
using EvadminAPI.DataBase.Models;
using EvadminAPI.DataBase.Repository.Extensions;
using EvadminAPI.Infrastucture.Extensions;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class StationController : Controller
	{
		private readonly IStationService _stationService;

		public StationController(IStationService stationService)
		{
			_stationService = stationService;
		}

		[Authorize(Roles = "owner")]
		[HttpGet("StationForm")]
		public async Task<IActionResult> GetStationForm() => View("station_form");

		[HttpPost]
		[Authorize(Roles = "owner")]          
		public async Task<IActionResult> CreateStation([FromBody] StationContract contract)
		{
			
			Guid ownerId = User.GetUserId();  

			var created = await _stationService.CreateAsync(ownerId, contract);
			return CreatedAtAction(nameof(GetStationById), new { id = created.Id }, created);
		}

		[HttpPut("{id:guid}")]
		[Authorize(Roles = "owner")]
		public async Task<IActionResult> UpdateStation(Guid id, [FromBody] StationContract contract)
		{
			Guid ownerId = User.GetUserId();
			var station = await _stationService.UpdateAsync(ownerId, id, contract);
			return Ok(station);
		}

		[HttpDelete("{id:guid}")]
		[Authorize(Roles = "owner")]
		public async Task<IActionResult> DeleteStation(Guid id)
		{
			var ownerId = User.GetUserId();
			await _stationService.DeleteAsync(ownerId, id);
			return NoContent();
		}

		[HttpGet("{id:guid}")]
		[Authorize(Roles = "owner,manager")]
		public async Task<IActionResult> GetStationById(Guid id)
		{
			var station = await _stationService.GetByIdAsync(User, id);
			if (station == null) return NotFound();
			return Ok(station);
		}

		[HttpGet]
		[Authorize(Roles = "owner,manager")]
		public async Task<IActionResult> GetAllStations()
		{
			var stations = await _stationService.GetAllAsync(User);
			if (stations == null) return NotFound();
			return Ok(stations);
		}

		[HttpPatch("{id:guid}/status")]
		[Authorize(Roles = "owner")]
		public async Task<IActionResult> UpdateStationStatus(Guid id)
		{
			await _stationService.UpdateStatusAsync(id);
			return NoContent();
		}

		[HttpGet("ProfileStations")]
		[Authorize(Roles = "owner")]
		public async Task<IActionResult> GetProfileStations()
		{
			var stations = await _stationService.GetAllAsync(User);
			return Ok(stations);
		}

	}
}

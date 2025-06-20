using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase;
using EvadminAPI.Infrastucture.Extensions;
using EvadminAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvadminAPI.Controllers
{
	[Controller]
	[Route("[controller]")]
	public class SessionController : Controller
	{
		private readonly MyApplicationContext _context;
		private readonly ISessionService _sessionService;

		public SessionController(ISessionService sessionService, MyApplicationContext context)
		{
			_sessionService = sessionService;
			_context = context;
		}

		[Authorize(Roles = "manager")]
		[HttpGet("SessionForm")]
		public async Task<IActionResult> GetSessionForm() => View("session_form");

		[HttpPost]
		[Authorize(Roles = "manager")]
		public async Task<IActionResult> CreateSession([FromBody] SessionContract contract)
		{
			Guid managerId = User.GetUserId();
			var session = await _sessionService.CreateAsync(managerId, contract);
			return CreatedAtAction(nameof(GetSessionById), new { id = session.Id }, session);
		}

		[HttpPatch("{id:guid}/status")]
		[Authorize(Roles = "manager")]
		public async Task<IActionResult> UpdateSessionStatus(Guid id)
		{
			await _sessionService.UpdateStatusAsync(id);
			return NoContent();
		}

		[HttpGet("{id:guid}")]
		[Authorize(Roles = "manager,owner")]
		public async Task<IActionResult> GetSessionById(Guid id)
		{
			var session = await _sessionService.GetByIdAsync(User, id);
			return session == null ? NotFound() : Ok(session);
		}

		[HttpGet("All")]
		[Authorize(Roles = "manager,owner")]
		public async Task<IActionResult> GetAllSessions()
		{
			var sessions = await _context.Sessions
				.Select(s => new {
					id = s.Id,
					number = s.Number,
					date = s.Date,
					client_name = s.Client_name,
					location = s.Location,
					info = s.Info,
					energy = s.Energy,
					cost = s.Cost,
					status = s.Status
				})
				.ToListAsync();

			return Ok(sessions);
		}

		[HttpGet("OwnerSessions")]
		[Authorize(Roles = "owner")]
		public async Task<IActionResult> GetOwnerSessions()
		{
			Guid ownerId = User.GetUserId();

			var sessions = await _context.Sessions
				.Where(s => s.Station.Owner_id == ownerId)
				.Select(s => new {
					id = s.Id,
					number = s.Number,
					date = s.Date,
					client_name = s.Client_name,
					location = s.Location,
					info = s.Info,
					energy = s.Energy,
					cost = s.Cost,
					status = s.Status
				})
				.ToListAsync();

			return Ok(sessions);
		}

		[HttpGet("ProfileSessions")]
		[Authorize(Roles = "manager")]
		public async Task<IActionResult> GetProfileSessions()
		{

			var sessions = await _context.Sessions
				.Select(s => new {
					id = s.Id,
					name = s.Number.ToString(),
					status = s.Status,
				})
				.ToListAsync();

			return Ok(sessions);
		}

	}

}

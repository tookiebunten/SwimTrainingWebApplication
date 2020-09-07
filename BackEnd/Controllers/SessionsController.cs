using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using EventsDTO;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SessionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<SessionResponse>>> Get()
        {
            var sessions = await _db.Sessions.AsNoTracking()
                                             .Include(s => s.Squads)
                                             .Include(s => s.SessionCoaches)
                                                .ThenInclude(ss => ss.Coaches)
                                             .Select(m => m.MapSessionResponse())
                                             .ToListAsync();

            return sessions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponse>> Get(int id)
        {
            var session = await _db.Sessions.AsNoTracking()
                                            .Include(s => s.Squads)
                                            .Include(s => s.SessionCoaches)
                                                .ThenInclude(ss => ss.Coaches)
                                            .SingleOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return session.MapSessionResponse();
        }

        [HttpPost]
        public async Task<ActionResult<SessionResponse>> Post(EventsDTO.Session input)
        {
            var session = new Data.Session
            {
                SessionTitle = input.SessionTitle,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                SessionDescription = input.SessionDescription,
                SquadId = input.SquadId
            };

            _db.Sessions.Add(session);
            await _db.SaveChangesAsync();

            var result = session.MapSessionResponse();

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventsDTO.Session input)
        {
            var session = await _db.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            session.Id = input.Id;
            session.SessionTitle = input.SessionTitle;
            session.SessionDescription = input.SessionDescription;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;
            session.SquadId = input.SquadId;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionResponse>> Delete(int id)
        {
            var session = await _db.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            _db.Sessions.Remove(session);
            await _db.SaveChangesAsync();

            return session.MapSessionResponse();
        }


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] ConferenceFormat format, IFormFile file)
        {
            var loader = GetLoader(format);

            using (var stream = file.OpenReadStream())
            {
                await loader.LoadDataAsync(stream, _db);
            }

            await _db.SaveChangesAsync();

            return Ok();
        }

        private static DataLoader GetLoader(ConferenceFormat format)
        {
            if (format == ConferenceFormat.Sessionize)
            {
                return new SessionizeLoader();
            }
            return new DevIntersectionLoader();
        }

        public enum ConferenceFormat
        {
            Sessionize,
            DevIntersections
        }
    }
}

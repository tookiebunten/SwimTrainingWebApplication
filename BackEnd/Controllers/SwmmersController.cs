using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EventsDTO;

namespace BackEnd.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SwimmersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SwimmersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<SwimmerResponse>> Get(string username)
        {
            var swimmer = await _context.Swimmers.Include(a => a.SessionSwimmers)
                                                .ThenInclude(sa => sa.Session)
                                              .SingleOrDefaultAsync(a => a.UserName == username);

            if (swimmer == null)
            {
                return NotFound();
            }

            var result = swimmer.MapSwimmerResponse();

            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<SwimmerResponse>> Post(EventsDTO.Swimmer input)
        {
            // Check if the Swimmer already exists
            var existingSwimmer = await _context.Swimmers
                .Where(a => a.UserName == input.UserName)
                .FirstOrDefaultAsync();

            if (existingSwimmer != null)
            {
                return Conflict(input);
            }

            var swimmer = new Data.Swimmer
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            _context.Swimmers.Add(swimmer);
            await _context.SaveChangesAsync();

            var result = swimmer.MapSwimmerResponse();

            return CreatedAtAction(nameof(Get), new { username = result.UserName }, result);
        }

        [HttpPost("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SwimmerResponse>> AddSession(string username, int sessionId)
        {
            var swimmer = await _context.Swimmers.Include(a => a.SessionSwimmers)
                                                .ThenInclude(sa => sa.Session)
                                              .SingleOrDefaultAsync(a => a.UserName == username);

            if (swimmer == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return BadRequest();
            }

            swimmer.SessionSwimmers.Add(new SessionSwimmer
            {
                SwimmerId = swimmer.Id,
                SessionId = sessionId
            });

            await _context.SaveChangesAsync();

            var result = swimmer.MapSwimmerResponse();

            return result;
        }

        [HttpDelete("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveSession(string username, int sessionId)
        {
            var swimmer = await _context.Swimmers.Include(a => a.SessionSwimmers)
                                              .SingleOrDefaultAsync(a => a.UserName == username);

            if (swimmer == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return BadRequest();
            }

            var sessionSwimmer = swimmer.SessionSwimmers.FirstOrDefault(sa => sa.SessionId == sessionId);
            swimmer.SessionSwimmers.Remove(sessionSwimmer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

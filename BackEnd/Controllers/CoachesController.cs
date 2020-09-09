using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Coaches
        [HttpGet]
        public async Task<ActionResult<List<EventsDTO.CoachResponse>>> GetSpeakers()
        {

            var coaches = await _context.Coaches.AsNoTracking()
                            .Include(s => s.SessionCoaches)
                                .ThenInclude(ss => ss.Session)
                                .Select(s => s.MapCoachResponse())
                            .ToListAsync();
            return coaches;
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventsDTO.CoachResponse>> GetCoach(int id)
        {
            var coach = await _context.Coaches.AsNoTracking()
                                            .Include(s => s.SessionCoaches)
                                                .ThenInclude(ss => ss.Session)
                                            .SingleOrDefaultAsync(s => s.Id == id);
            if (coach == null)
            {
                return NotFound();
            }
            return coach.MapCoachResponse();
        }
    }
}


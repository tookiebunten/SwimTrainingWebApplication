﻿using System;
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
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoaches()
        {
            return await _context.Coaches.ToListAsync();
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventsDTO.CoachResponse>> GetSpeaker(int id)
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

        // PUT: api/Coaches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            if (id != coach.Id)
            {
                return BadRequest();
            }

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coaches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Coach>> PostCoach(Coach coach)
        {
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoach", new { id = coach.Id }, coach);
        }

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Coach>> DeleteCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return coach;
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }
    }
}

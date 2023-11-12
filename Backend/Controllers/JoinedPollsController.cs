using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Learning.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinedPollsController : ControllerBase
    {
        private readonly GeneralContext _context;

        public JoinedPollsController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/JoinedPolls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JoinedPoll>>> GetJoinedPoll()
        {
            return await _context.JoinedPoll.ToListAsync();
        }

        // GET: api/JoinedPolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JoinedPoll>> GetJoinedPoll(int id)
        {
            var joinedPoll = await _context.JoinedPoll.FindAsync(id);

            if (joinedPoll == null)
            {
                return NotFound();
            }

            return joinedPoll;
        }

        // PUT: api/JoinedPolls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoinedPoll(int id, JoinedPoll joinedPoll)
        {
            if (id != joinedPoll.PoolId)
            {
                return BadRequest();
            }

            _context.Entry(joinedPoll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoinedPollExists(id))
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

        // POST: api/JoinedPolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JoinedPoll>> PostJoinedPoll(JoinedPoll joinedPoll)
        {
            _context.JoinedPoll.Add(joinedPoll);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JoinedPollExists(joinedPoll.PoolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJoinedPoll", new { id = joinedPoll.PoolId }, joinedPoll);
        }

        // DELETE: api/JoinedPolls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoinedPoll(int id)
        {
            var joinedPoll = await _context.JoinedPoll.FindAsync(id);
            if (joinedPoll == null)
            {
                return NotFound();
            }

            _context.JoinedPoll.Remove(joinedPoll);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JoinedPollExists(int id)
        {
            return _context.JoinedPoll.Any(e => e.PoolId == id);
        }
    }
}

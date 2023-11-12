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
    public class RequestJoinsController : ControllerBase
    {
        private readonly GeneralContext _context;

        public RequestJoinsController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/RequestJoins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestJoin>>> GetRequestJoin()
        {
            return await _context.RequestJoin.ToListAsync();
        }

        // GET: api/RequestJoins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestJoin>> GetRequestJoin(int id)
        {
            var requestJoin = await _context.RequestJoin.FindAsync(id);

            if (requestJoin == null)
            {
                return NotFound();
            }

            return requestJoin;
        }

        // PUT: api/RequestJoins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestJoin(int id, RequestJoin requestJoin)
        {
            if (id != requestJoin.PoolId)
            {
                return BadRequest();
            }

            _context.Entry(requestJoin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestJoinExists(id))
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

        // POST: api/RequestJoins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestJoin>> PostRequestJoin(RequestJoin requestJoin)
        {
            _context.RequestJoin.Add(requestJoin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestJoin", new { id = requestJoin.PoolId }, requestJoin);
        }

        // DELETE: api/RequestJoins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestJoin(int id)
        {
            var requestJoin = await _context.RequestJoin.FindAsync(id);
            if (requestJoin == null)
            {
                return NotFound();
            }

            _context.RequestJoin.Remove(requestJoin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestJoinExists(int id)
        {
            return _context.RequestJoin.Any(e => e.PoolId == id);
        }
    }
}

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
    public class PoolsController : ControllerBase
    {
        private readonly GeneralContext _context;

        public PoolsController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Pools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pool>>> GetPool()
        {
            return await _context.Pool.ToListAsync();
        }

        // GET: api/Pools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pool>> GetPool(int id)
        {
            var pool = await _context.Pool.FindAsync(id);

            if (pool == null)
            {
                return NotFound();
            }

            return pool;
        }

        // PUT: api/Pools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPool(int id, Pool pool)
        {
            if (id != pool.PoolId)
            {
                return BadRequest();
            }

            _context.Entry(pool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExists(id))
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

        // POST: api/Pools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pool>> PostPool(Pool pool)
        {
            _context.Pool.Add(pool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPool", new { id = pool.PoolId }, pool);
        }

        // DELETE: api/Pools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool(int id)
        {
            var pool = await _context.Pool.FindAsync(id);
            if (pool == null)
            {
                return NotFound();
            }

            _context.Pool.Remove(pool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoolExists(int id)
        {
            return _context.Pool.Any(e => e.PoolId == id);
        }
    }
}

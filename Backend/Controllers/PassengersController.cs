using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Learning.Models;
using Backend.Models.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly GeneralContext _context;

        public PassengersController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassenger()
        {
            return await _context.Passenger.ToListAsync();
        }

        // GET: api/Passengers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
            var passenger = await _context.Passenger.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
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

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(Passenger passenger)
        {
            _context.Passenger.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = passenger.PassengerId }, passenger);
        }

        // DELETE: api/Passengers/5
        //  PASSENGER LEAVE
        [HttpDelete("PassengerLeave")]
        public async Task<IActionResult> DeletePassenger(PassengerDTO dto)
        {
            var temp = _context.Passenger
               .Where(x => x.PoolId == dto.PoolId
               && x.PassengerId == dto.PassengerId).FirstOrDefault();

            if (temp == null)
            {
                return NotFound();
            }

            _context.Passenger.Remove(temp);
            await _context.SaveChangesAsync();

            UpdatePool(dto.PoolId);

            return NoContent();
        }

        private bool PassengerExists(int id)
        {
            return _context.Passenger.Any(e => e.PassengerId == id);
        }

        private async void UpdatePool(int id)
        {
            //int id = request.PoolId;
            Pool pool = await _context.Pool.FindAsync(id);

            // GENERATE NEW ROUTE
            pool.RouteId = 0;

            _context.Entry(pool).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }
    }
}

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
using Humanizer;

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

        // GET: api/RequestJoins/5
        [HttpGet("GetAllPendingRequest")]
        public async Task<ActionResult<IEnumerable<RequestJoin>>> GetAllPendingRequest(int id)
        {
            var temp = await _context.RequestJoin
               .Where(x => x.PoolId == id).ToListAsync();
            if (temp == null)
            {
                return NotFound();
            }

            return temp;
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
        // ADD REQUEST JOIN
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestJoin>> PostRequestJoin(RequestDTO dto)
        {

            RequestJoin obj = dtoToRequest(dto);
            _context.RequestJoin.Add(obj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestJoin", new { id = obj.PoolId }, obj);
        }

        // DELETE: api/RequestJoins/5
        // ACCEPT REQUEST 
        [HttpDelete("AcceptRequest")]
        public async Task<IActionResult> AcceptRequestJoin(RequestJoin requestJoin)
        {
            var temp = _context.RequestJoin
               .Where(x => x.PoolId == requestJoin.PoolId
               && x.MemId == requestJoin.MemId).FirstOrDefault();

            if (temp == null)
            {
                return NotFound();
            }
            _context.Passenger.Add(requestToPassenger(requestJoin));            // Add passenger
            _context.JoinedPoll.Add(RequestPollToJoinedPoll(requestJoin));      // Add JoinedPoll
            _context.RequestJoin.Remove(temp);
            await _context.SaveChangesAsync();

            // Update route in pool
            UpdatePool(requestJoin);

            return NoContent();
        }

        // DELETE: api/RequestJoins/5
        // DELETE REQUEST OR REJECT
        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequestJoin(RequestJoin requestJoin)
        {
            var temp = _context.RequestJoin
               .Where(x => x.PoolId == requestJoin.PoolId
               && x.MemId == requestJoin.MemId).FirstOrDefault();
            if (temp == null)
            {
                return NotFound();
            }

            _context.RequestJoin.Remove(temp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestJoinExists(int id)
        {
            return _context.RequestJoin.Any(e => e.PoolId == id);
        }

        private RequestJoin dtoToRequest(RequestDTO dto)
        {
            RequestJoin obj = new RequestJoin
            {
                PoolId = dto.PoolId,
                MemId = dto.MemId,
                PickupTime = "",            // NEED ROUTE
                RouteId = 0
            };

            return obj;
        }

        private Passenger requestToPassenger(RequestJoin request)
        {
            Passenger passenger = new Passenger
            {
                PoolId = request.PoolId,
                PassengerId = request.MemId,
                PickupTime = request.PickupTime
            };

            return passenger;
        }

        private JoinedPoll RequestPollToJoinedPoll(RequestJoin requestJoin)
        {
            JoinedPoll joined = new JoinedPoll
            {
                MemId = requestJoin.MemId,
                PoolId = requestJoin.PoolId,
                PickupTime = requestJoin.PickupTime,
                RouteId = requestJoin.RouteId,
            };

            return joined;
        }

        private async void UpdatePool(RequestJoin request)
        {
            //int id = request.PoolId;
            Pool pool = await _context.Pool.FindAsync(request.PoolId);

            pool.RouteId = request.RouteId;

            _context.Entry(pool).State = EntityState.Modified;

            await _context.SaveChangesAsync();  

        }

        private bool PoolExists(int id)
        {
            return _context.Pool.Any(e => e.PoolId == id);
        }
    }
}

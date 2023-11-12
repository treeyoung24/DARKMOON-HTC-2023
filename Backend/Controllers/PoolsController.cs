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
        // VIEW POLL BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PoolViewDTO>> GetPool(int id)
        {
            var pool = await _context.Pool.FindAsync(id);

            if (pool == null)
            {
                return NotFound();
            }

            User user = await _context.Users.FindAsync(pool.HostId);
            PoolViewDTO dto = PoolToViewPool(pool);
            dto.StartingPoint = user.Address;

            return dto;
        }

        // GET: api/Pools/5
        //[HttpGet("GetAllUserPools")]
        //public async Task<ActionResult<IEnumerable<Pool>>> GetAllUserPools(int id)
        //{
            //var driver = await GetDriverPools(id);
            //var passenger = await GetPassengerPools(id);

            //var combinedPools = driver.Value.Concat(passenger.Value);

            //return Ok(combinedPools);
        //}

        //GET: api/Pools/5
        [HttpGet("GetDriverPools")]
        public async Task<ActionResult<IEnumerable<PoolDriverMyPool>>> GetDriverPools(int id)
        {
            var temp = await _context.Pool
               .Where(x => x.HostId == id).ToListAsync();

            if (temp == null)
            {
                return NotFound();
            }

            List<PoolDriverMyPool> listPools = new List<PoolDriverMyPool> ();

            foreach(Pool p in temp)
            {
                PoolDriverMyPool dp = PoolToDiverPool(p);
                var pass = await _context.Passenger.Where(x => x.PoolId == p.PoolId).ToListAsync();
                dp.AvailableSlot = dp.PoolSize - pass.Count();
                dp.TotalEarn = pass.Count() * 3.25f;
                listPools.Add(dp);
            }

            return listPools;
        }

        //GET: api/Pools/5
        [HttpGet("GetPassengerPools")]
        public async Task<ActionResult<IEnumerable<PoolPassengerMyView>>> GetPassengerPools(int id)
        {
            var passenger = await _context.Passenger
               .Where(x => x.PassengerId == id).ToListAsync(); 

            if (passenger.Count == 0 || passenger == null)
            {
                return NotFound();
            }

            
            List<PoolPassengerMyView> pools = new List<PoolPassengerMyView>();
            foreach (var t in passenger) // Specify the type of 't'
            {
                var pool = await _context.Pool.FindAsync(t.PoolId);
                if (pool != null)
                {
                    pools.Add(PoolToPassengerView(pool));
                }
            }

            return pools;
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
        // CREATE POOL BY DRIVER
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pool>> PostPool(PoolDTO dto)
        {
            (Pool obj, Driver dr) = dtoToPool(dto);

            // Add pool
            _context.Pool.Add(obj);
            await _context.SaveChangesAsync();

            // Add driver
            _context.Driver.Add(dr);
            await _context.SaveChangesAsync();

            // Generate Route

            return CreatedAtAction("GetPool", new { id = obj.PoolId }, obj);
        }


        // DELETE: api/Pools/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePool(DriverDTO dto)
        {
            var temp = _context.Pool
               .Where(x => x.PoolId == dto.PoolID
               && x.HostId == dto.DriverID).FirstOrDefault();
            
            
            if (temp == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver.FindAsync(dto.PoolID);
            _context.Pool.Remove(temp);
            _context.Driver.Remove(driver);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pools/5
        [HttpDelete("Out")]
        public async Task<IActionResult> DeletePool(PassengerDTO dto)
        {
            var temp = _context.Passenger
               .Where(x => x.PoolId == dto.PoolId   
               && x.PassengerId == dto.PassengerId).FirstOrDefault();


            if (temp == null)
            {
                return NotFound();
            }

            var pool = await _context.Pool.FindAsync(dto.PoolId);
            _context.Pool.Remove(pool);
            _context.Passenger.Remove(temp);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoolExists(int id)
        {
            return _context.Pool.Any(e => e.PoolId == id);
        }


        [NonAction]
        public int GenerateRandomPoolId()
        {
            Random random = new Random();

            // Generate a random 8-digit number
            int poolId = random.Next(10000000, 99999999);

            return poolId;
        }

        private (Pool, Driver) dtoToPool(PoolDTO dto)
        {
            Pool obj = new Pool();

            int rand = GenerateRandomPoolId();
            obj.PoolId = rand;
            obj.PoolSize = dto.PoolSize;
            obj.ArrivalTime = dto.ArrivalTime;
            obj.Destination = dto.Destination;
            obj.HostId = dto.HostId;
            obj.RouteId = 0;

            Driver dr = new Driver
            {
                DriverId = dto.HostId,
                StartTime = "0",  // TODO
                PoolId = rand  // Set PoolId from the newly added Pool
            };

            return (obj, dr);
        }

        private PoolViewDTO PoolToViewPool(Pool pool)
        {
            

            PoolViewDTO result = new PoolViewDTO
            {
                PoolId = pool.PoolId,
                PoolSize = pool.PoolSize,
                ArrivalTime = pool.ArrivalTime,
                Destination = pool.Destination,
                StartingPoint = ""
            };

            return result;
        }

        private PoolDriverMyPool PoolToDiverPool(Pool pool)
        {
            PoolDriverMyPool driverPool = new PoolDriverMyPool
            {
                ArrivalTime = pool.ArrivalTime,
                PoolId = pool.PoolId,
                PoolSize = pool.PoolSize,
                Destination = pool.Destination,
                // TODO START TIME

            };

            return driverPool;
        }

        private PoolPassengerMyView PoolToPassengerView(Pool pool)
        {
            PoolPassengerMyView passPool = new PoolPassengerMyView
            {
                ArrivalTime = pool.ArrivalTime,
                PoolId = pool.PoolId,
                CO2 = 250,
                Fee = 3.25f,
            };

            return passPool;
        }

    }


}

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
    public class RoutesController : ControllerBase
    {
        private readonly GeneralContext _context;

        public RoutesController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/Routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routes>>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        // GET: api/Routes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDTO>> GetRoutes(int id)
        {
            Pool pool = await _context.Pool.FindAsync(id);

            if (pool == null)
            {
                return NotFound();
            }
            var routes = await _context.Routes.FindAsync(pool.RouteId);
            List<RouteOrderDTO> seg = new List<RouteOrderDTO>();
            List<RouteOrder> routeOrders = await _context.RouteOrder.Where(x => x.RouteId == pool.RouteId).ToListAsync();
            foreach (RouteOrder routeOrder in routeOrders)
            {
                seg.Add(new RouteOrderDTO
                {
                    UserID = routeOrder.UserId,
                    Address = (await _context.Users.FindAsync(routeOrder.UserId)).Address,
                    Order = routeOrder.Order
                });
            }
            return new RouteDTO
            {
                RouteId = pool.RouteId, 
                Distance = routes.Distance,
                Duration = routes.Duration,
                Polylines = routes.Polylines,
                Segments = seg,
            };

        }

        // PUT: api/Routes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutes(int id, Routes routes)
        {
            if (id != routes.RouteId)
            {
                return BadRequest();
            }

            _context.Entry(routes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutesExists(id))
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

        // POST: api/Routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Routes>> PostRoutes(Routes routes)
        {
            _context.Routes.Add(routes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoutes", new { id = routes.RouteId }, routes);
        }

        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutes(int id)
        {
            var routes = await _context.Routes.FindAsync(id);
            if (routes == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(routes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoutesExists(int id)
        {
            return _context.Routes.Any(e => e.RouteId == id);
        }
    }
}

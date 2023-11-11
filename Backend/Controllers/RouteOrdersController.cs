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
    public class RouteOrdersController : ControllerBase
    {
        private readonly GeneralContext _context;

        public RouteOrdersController(GeneralContext context)
        {
            _context = context;
        }

        // GET: api/RouteOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteOrder>>> GetRouteOrder()
        {
            return await _context.RouteOrder.ToListAsync();
        }

        // GET: api/RouteOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteOrder>> GetRouteOrder(int id)
        {
            var routeOrder = await _context.RouteOrder.FindAsync(id);

            if (routeOrder == null)
            {
                return NotFound();
            }

            return routeOrder;
        }

        // PUT: api/RouteOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteOrder(int id, RouteOrder routeOrder)
        {
            if (id != routeOrder.Order)
            {
                return BadRequest();
            }

            _context.Entry(routeOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteOrderExists(id))
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

        // POST: api/RouteOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RouteOrder>> PostRouteOrder(RouteOrder routeOrder)
        {
            _context.RouteOrder.Add(routeOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRouteOrder", new { id = routeOrder.Order }, routeOrder);
        }

        // DELETE: api/RouteOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouteOrder(int id)
        {
            var routeOrder = await _context.RouteOrder.FindAsync(id);
            if (routeOrder == null)
            {
                return NotFound();
            }

            _context.RouteOrder.Remove(routeOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteOrderExists(int id)
        {
            return _context.RouteOrder.Any(e => e.Order == id);
        }
    }
}

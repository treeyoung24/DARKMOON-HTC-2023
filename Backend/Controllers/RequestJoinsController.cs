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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;

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
        [HttpGet("GetSingleRequest")]
        public async Task<ActionResult<IEnumerable<RequestJoin>>> GetSingleRequest(int id, int MemId)
        {
            var temp = await _context.RequestJoin
               .Where(x => x.PoolId == id && x.MemId == MemId).ToListAsync();

            if (temp == null)
            {
                return NotFound();
            }

            return temp;
        }

        // GET: api/RequestJoins/5
        [HttpGet("GetPassengerRequest")]
        public async Task<ActionResult<IEnumerable<PoolPassengerMyView>>> GetPassengerRequest(int MemId)
        {
            var temp = await _context.RequestJoin
               .Where(x => x.MemId == MemId).ToListAsync();

            if (temp == null)
            {
                return NotFound();
            }

            List<PoolPassengerMyView> pools = new List<PoolPassengerMyView>();
            foreach (RequestJoin pv in temp)
            {
                
                var temp2 = RequestToPassengerView(pv);
                Pool pool = await _context.Pool.FindAsync(temp2.PoolId);
                if (pool != null)
                {
                    var pass = await _context.Passenger.Where(x => x.PoolId == pool.PoolId && x.PassengerId != MemId).ToListAsync();

                    temp2.ArrivalTime = pool.ArrivalTime;
                    temp2.Stops = pass.Count;

                    pools.Add(temp2);
                }
                    
            }

            

            return pools;
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

            List<Passenger> passengerList = await _context.Passenger
               .Where(x => x.PoolId == dto.PoolId).ToListAsync();

            List<String> addressList = new List<string>();
            foreach(Passenger p in  passengerList)
            {
                addressList.Add((await _context.Users.FindAsync(p.PassengerId)).Address);
            }
            String startPoint = await getStartAddressFromPoolId(obj.PoolId);
            Pool pool = await _context.Pool.FindAsync(obj.PoolId);
            int minInt = 0;
            HttpResponseMessage savedResponse= null;
            int recordedI = 0;
            for (int i = 0; i < passengerList.Count; i++)
            {
                Console.WriteLine(0);
                List<String> aList = new List<String>(addressList);
                aList.Insert(i, (await _context.Users.FindAsync(obj.MemId)).Address);
                var addressJsonList = aList.Select(x => new { address = x }).ToArray();
                // prepare request
                var client = new HttpClient();
                String url = "https://routes.googleapis.com/directions/v2:computeRoutes?key=" + Environment.GetEnvironmentVariable("API_KEY");
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                var routeRequest = new
                {
                    origin = new { address = startPoint, sideOfRoad = true },
                    destination = new { address = pool.Destination },
                    intermediates = addressJsonList,
                    travelMode = "DRIVE",
                    routingPreference = "TRAFFIC_AWARE_OPTIMAL",
                    arrivalTime = pool.ArrivalTime,
                    computeAlternativeRoutes = false,
                    routeModifiers = new { vehicleInfo = new { emissionType = "GASOLINE" } },
                    languageCode = "en-US",
                    units = "IMPERIAL"
                };

                //Console.Write(JsonConvert.SerializeObject(routeRequest));
                //Console.Write("\n");

                httpRequest.Content = new StringContent(
                    JsonConvert.SerializeObject(routeRequest), Encoding.UTF8, "application/json");
                httpRequest.Headers.Add("X-Goog-FieldMask",
                    "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline,routes.legs.duration");
                HttpResponseMessage response = await client.SendAsync(httpRequest);
                dynamic item = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                // Generate Route
                int duration = Int32.Parse(
                    item.routes[0].duration.ToString().Remove(item.routes[0].duration.ToString().Length - 1)
                    );


                if(i == 0)
                {
                    savedResponse = response; ;
                    minInt = duration;
                    recordedI = i;
                    Console.WriteLine(1);
                }
                if(minInt < duration)
                {
                    savedResponse = response; ;
                    minInt = duration;
                    recordedI = i;
                    Console.WriteLine(2);
                }
            }
            if(addressList.Count == 0)
            {
                List<String> aList = new List<String>();

                aList.Add((await _context.Users.FindAsync(obj.MemId)).Address);
                var addressJsonList = aList.Select(x => new { address = x }).ToArray();
                // prepare request
                var client = new HttpClient();
                String url = "https://routes.googleapis.com/directions/v2:computeRoutes?key=" + Environment.GetEnvironmentVariable("API_KEY");
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                var routeRequest = new
                {
                    origin = new { address = startPoint, sideOfRoad = true },
                    destination = new { address = pool.Destination },
                    intermediates = addressJsonList,
                    travelMode = "DRIVE",
                    routingPreference = "TRAFFIC_AWARE_OPTIMAL",
                    arrivalTime = pool.ArrivalTime,
                    computeAlternativeRoutes = false,
                    routeModifiers = new { vehicleInfo = new { emissionType = "GASOLINE" } },
                    languageCode = "en-US",
                    units = "IMPERIAL"
                };

                Console.Write(JsonConvert.SerializeObject(routeRequest));
                //Console.Write("\n");

                httpRequest.Content = new StringContent(
                    JsonConvert.SerializeObject(routeRequest), Encoding.UTF8, "application/json");
                httpRequest.Headers.Add("X-Goog-FieldMask",
                    "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline,routes.legs.duration");
                HttpResponseMessage response = await client.SendAsync(httpRequest);
                Console.Write((await response.Content.ReadAsStringAsync()));
                dynamic item = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                // Generate Route
                int duration = Int32.Parse(
                    item.routes[0].duration.ToString().Remove(item.routes[0].duration.ToString().Length - 1)
                    );

                    savedResponse = response; ;
                    minInt = duration;
                    recordedI = 0;
                    Console.WriteLine(2);
            }
            Console.Write(savedResponse.ToString());
            dynamic newitem = JsonConvert.DeserializeObject(await savedResponse.Content.ReadAsStringAsync());
            int newduration = Int32.Parse(
                newitem.routes[0].duration.ToString().Remove(newitem.routes[0].duration.ToString().Length - 1)
                );
            Random random = new Random();
            Routes newRoute = new Routes
            {
                RouteId = random.Next(10000000, 99999999),
                Distance = newitem.routes[0].distanceMeters,
                Duration = newduration,
                Polylines = newitem.routes[0].polyline.encodedPolyline
            };

            addressList.Insert(recordedI, (await _context.Users.FindAsync(obj.MemId)).Address);

            obj.RouteId = newRoute.RouteId;

            int time = 0;
            for (int i = 0;i < addressList.Count; i++)
            {
                if(i >= recordedI)
                {
                    var k = newitem.routes[0].legs[i];
                    time += Int32.Parse(k.duration.ToString().Remove(k.duration.ToString().Length - 1));
                }

                RouteOrder newOrder = new RouteOrder();
                newOrder.RouteId = newRoute.RouteId;
                newOrder.UserId =  _context.Users.Where(x => x.Address.Equals(addressList[i])).FirstOrDefault().UserId;
                newOrder.Order = i;
                _context.RouteOrder.Add(newOrder);
                await _context.SaveChangesAsync();
            }
            DateTime dateTimeOffset = DateTime.Parse(pool.ArrivalTime, null, DateTimeStyles.RoundtripKind);
            
            obj.PickupTime = dateTimeOffset.AddSeconds(0-time).ToString(); 

            _context.Routes.Add(newRoute);
            await _context.SaveChangesAsync();

            _context.RequestJoin.Add(obj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestJoin", new { id = obj.PoolId }, obj);
        }

        private async Task<String> getStartAddressFromPoolId(int poolId)
        {
            Pool pool = await _context.Pool.FindAsync(poolId);
            User user = await _context.Users.FindAsync(pool.HostId);
            return user.Address;
        }

        // DELETE: api/RequestJoins/5
        // ACCEPT REQUEST 
        [HttpDelete("AcceptRequest")]
        public async Task<IActionResult> AcceptRequestJoin(RequestDTO requestJoin)
        {
            var temp = _context.RequestJoin
               .Where(x => x.PoolId == requestJoin.PoolId
               && x.MemId == requestJoin.MemId).FirstOrDefault();

            if (temp == null)
            {
                return NotFound();
            }

            _context.Passenger.Add(requestToPassenger(temp));            // Add passenger
            _context.JoinedPoll.Add(RequestPollToJoinedPoll(temp));      // Add JoinedPoll
            _context.RequestJoin.Remove(temp);
            await _context.SaveChangesAsync();

            // Update route in pool
            UpdatePool(temp);

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

        private PoolPassengerMyView RequestToPassengerView(RequestJoin requestJoin)
        {
            

            PoolPassengerMyView passPool = new PoolPassengerMyView
            {
                PoolId = requestJoin.PoolId,
                CO2 = 250,
                Fee = 3.25f,
            };

            return passPool;
        }

        private async void UpdatePool(RequestJoin request)
        {
            //int id = request.PoolId;
            Pool pool = await _context.Pool.FindAsync(request.PoolId);

            pool.RouteId = request.RouteId;
            _context.Entry(pool).State = EntityState.Modified;
            Routes route = await _context.Routes.FindAsync(request.RouteId);
            // Driver
            Driver dr = _context.Driver
               .Where(x => x.PoolId == pool.PoolId
               && x.DriverId == pool.HostId).FirstOrDefault(); 

            DateTime arrTime = DateTime.Parse(pool.ArrivalTime, null, DateTimeStyles.RoundtripKind);
            
            dr.StartTime = arrTime.AddSeconds(0 - route.Duration).ToString();

            _context.Entry(dr).State = EntityState.Modified;


            await _context.SaveChangesAsync();  

        }

        private bool PoolExists(int id)
        {
            return _context.Pool.Any(e => e.PoolId == id);
        }
    }
}

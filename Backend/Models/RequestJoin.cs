using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class RequestJoin
    {
        [Key, Column(Order = 0)]
        public int PoolId { get; set; }

        [Key, Column(Order = 1)]
        public int MemId { get; set; }

        public string PickupTime { get; set; }
        public int RouteId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class JoinedPoll
    {
        [Key, Column(Order = 0)]
        public int PoolId { get; set; }

        [Key, Column(Order = 1)]
        public int MemId { get; set; }

        public string PickupTime { get; set; }
        public int RouteId { get; set; }
    }
}

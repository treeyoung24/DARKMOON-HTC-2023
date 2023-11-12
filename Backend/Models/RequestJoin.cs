using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class RequestJoin
    {
        [Key]
        public int PoolId { get; set; }
        public int MemId { get; set; }
        public string PickupTime { get; set; }
        public int RouteId { get; set; }
    }
}

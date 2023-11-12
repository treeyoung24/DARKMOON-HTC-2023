using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Driver
    {
        [Key]
        public int PoolId { get; set; }
        public int DriverId { get; set; }
        public string StartTime { get; set; }
    }
}

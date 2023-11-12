using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Passenger
    {
        [Key, Column(Order = 0)]
        public int PoolId { get; set; }

        [Key, Column(Order = 1)]
        public int PassengerId { get; set; }
        public string PickupTime { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class RouteOrder
    {
        [Key, Column(Order = 0)]
        public int Order { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        [Key, Column(Order = 2)]
        public int RouteId { get; set; }
    }
}

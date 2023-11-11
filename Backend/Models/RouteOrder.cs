using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class RouteOrder
    {
        [Key]
        public int Order { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
    }
}

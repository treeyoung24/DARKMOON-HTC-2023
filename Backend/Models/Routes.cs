using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Routes
    {
        [Key]
        public int RouteId { get; set; }
        public float Distance { get; set; }
        public int Duration { get; set; }
        public string Polylines { get; set; }
    }
}

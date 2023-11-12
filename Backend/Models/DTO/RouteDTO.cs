namespace Backend.Models.DTO
{
    public class RouteDTO
    {
        public int RouteId { get; set; }
        public float Distance { get; set; }
        public int Duration { get; set; }
        public string Polylines { get; set; }

        public List<RouteOrderDTO> Segments { get; set; }
    }

    public class RouteOrderDTO
    {
        public int UserID { get; set; }
        public string Address { get; set; }
        public int Order { get; set; }
    }
}

namespace Backend.Models.DTO
{
    public class PoolViewDTO
    {
        public int PoolId { get; set; }
        public int PoolSize { get; set; }
        public string ArrivalTime { get; set; }
        public string Destination { get; set; }
        public string StartingPoint { get; set; }
    }
}

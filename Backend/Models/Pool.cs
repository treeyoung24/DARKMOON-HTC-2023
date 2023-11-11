namespace Backend.Models
{
    public class Pool
    {
        public int PoolId { get; set; }
        public int HostId { get; set; }
        public int PoolSize { get; set; }
        public string ArrivalTime { get; set; }
        public string Destination { get; set;}
    }
}

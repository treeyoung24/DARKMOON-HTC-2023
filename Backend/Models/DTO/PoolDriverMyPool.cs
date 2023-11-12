namespace Backend.Models.DTO
{
    public class PoolDriverMyPool
    {
        public string StartTime {  get; set; }
        public string ArrivalTime { get; set; }
        public int PoolSize { get; set;}
        public int AvailableSlot {  get; set; }
        public string Destination { get; set; }
        public float TotalEarn { get; set;}
        public int PoolId { get; set;}
    }
}

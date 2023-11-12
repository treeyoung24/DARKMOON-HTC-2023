namespace Backend.Models.DTO
{
    public class PoolPassengerMyView
    {
        public int PoolId { get; set; }
        public string PickupTime { get; set; }
        public string ArrivalTime { get; set; }
        public float CO2 { get; set; }
        public float Stops { get; set; }
        public float Fee { get; set; }
    }
}

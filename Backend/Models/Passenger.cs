namespace Backend.Models
{
    public class Passenger
    {
        public int PoolId { get; set; }
        public int PassengerId { get; set; }
        public string PickupTime { get; set; }
        public float Fee {  get; set; }
    }
}

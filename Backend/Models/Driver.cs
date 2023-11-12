using System.ComponentModel.DataAnnotations;
﻿using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models
{
    public class Driver
    {
        [Key, Column(Order = 0)]
        public int PoolId { get; set; }

        [Key, Column(Order = 1)]
        public int DriverId { get; set; }
        public string StartTime { get; set; }
    }
}

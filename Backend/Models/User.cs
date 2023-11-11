﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}

﻿namespace PSchool.Backend.Models
{
    public class Parent
    {
        public int Id { get; set; }

        public string WorkPhone { get; set; } = string.Empty;

        public string HomeAddress { get; set; } = string.Empty;

        public int Siblings { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relation properties

        public string UserId { get; set; } 

        // Navigation properties
        public User User { get; set; }
        public List<Student> Students { get; set; }

    }
}

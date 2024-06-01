using Newtonsoft.Json;
using PSchool.Backend.Models;

namespace PSchool.Backend.DataTransferObjects
{
    public class ParentDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string WorkPhone { get; set; } = string.Empty;

        public string HomeAddress { get; set; } = string.Empty;

        public int Siblings { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relation properties

        public string? UserId { get; set; }

        // Navigation properties
  
        public virtual User? User { get; set; } 
        public ICollection<Student>? Students { get; set; }

    }
}

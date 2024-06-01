using Newtonsoft.Json;

namespace PSchool.Backend.Models
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

        public string? UserId { get; set; }

        // Navigation properties
        
        public virtual User? User { get; set; } = null!;
        public ICollection<Student>? Students { get; }  = new List<Student>();

        public string? FullName { get; set; } = string.Empty;





    }
}

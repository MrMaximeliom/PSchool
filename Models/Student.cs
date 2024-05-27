

using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace PSchool.Backend.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relation properties 
        public int ParentId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Parent Parent { get; set; } = null!;


        public string ParentName { get; set; }
    }
}

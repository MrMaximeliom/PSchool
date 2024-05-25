using Newtonsoft.Json;
using PSchool.Backend.Models;

namespace PSchool.Backend.DataTransferObjects
{
    public class UserDto
    {
        public Guid Id { get; set; }  
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime RegistrationDateTime { get; set; } = DateTime.UtcNow;

        public DateTime LastLoginDateTime { get; set; }

        // Relation properties

        // Navigation properties


        [JsonIgnore]
        public virtual Parent Parent { get; set; } = new Parent();

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}

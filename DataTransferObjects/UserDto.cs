using Newtonsoft.Json;
using PSchool.Backend.Models;

namespace PSchool.Backend.DataTransferObjects
{
    public class UserDto
    {
        public string? Id { get; set; }  = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;
        
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime RegistrationDateTime { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginDateTime { get; set; }

        public string? Password { get;set; } = string.Empty; 


        [JsonIgnore]
        public virtual Parent? Parent { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }


    }
}

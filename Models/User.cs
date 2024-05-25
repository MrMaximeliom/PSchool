using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace PSchool.Backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime RegistrationDateTime { get; set; } = DateTime.UtcNow;

        public DateTime LastLoginDateTime { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Relation properties

        // Navigation properties
/*
        [JsonIgnore]
        public virtual Parent Parent { get; set; } = new Parent();*/


        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        public string FullName => FirstName + " " + LastName;

    }



}

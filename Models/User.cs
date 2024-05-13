using Microsoft.AspNetCore.Identity;

namespace PSchool.Backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime RegistrationDateTime { get; set; } = DateTime.UtcNow;

        public DateTime LastLoginDateTime { get; set; }

        // Relation properties

        // Navigation properties

        public List<Parent> Parents { get; set; }

        public List<Student> Students { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }





    }



}

using System.ComponentModel.DataAnnotations;

namespace PSchool.Backend.Models
{
    public class RequestToken
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using PSchool.Backend.Interfaces;
using PSchool.Backend.Models;

namespace PSchool.Backend.Repositories
{
    public class UserRepository<T> : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }


    }
}

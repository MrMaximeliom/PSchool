using Mapster;
using Microsoft.EntityFrameworkCore;
using PSchool.Backend.DataTransferObjects;
using PSchool.Backend.Models;

namespace PSchool.Backend.Repositories
{
    public class ParentRepository<T> : BaseRepository<Parent>
    {
        public ParentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Parent>> GetParentsWithDetails()
        {
            return await _context.Parents.Include(p => p.User).Select(
                p => new Parent
                {
                    Id = p.Id,
                    FullName = p.User.FullName,
                    WorkPhone = p.WorkPhone,
                    CreatedAt = p.CreatedAt,
                    Siblings = p.Siblings,



                }

                ).ToListAsync();
        }
        public  async Task<Parent?> GetParentWithDetailsById(int id)
        {
            return await _context.Parents.Where(p => p.Id == id).Include(p => p.User).FirstOrDefaultAsync();
            
        }
    }
}

using PSchool.Backend.Interfaces;
using PSchool.Backend.Models;

namespace PSchool.Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UserRepository<User> Users { get; private set; }
        public ParentRepository<Parent> Parents { get; private set; }
        public IBaseRepository<Student> Students { get; private set;}


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository<User>(_context);
            Parents = new ParentRepository<Parent>(_context);
            Students = new BaseRepository<Student>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();   
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

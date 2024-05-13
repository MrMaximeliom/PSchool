using PSchool.Backend.Models;

namespace PSchool.Backend.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Parent> Parents { get; }
        int Complete();

    }
}

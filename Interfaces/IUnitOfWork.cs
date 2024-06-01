using PSchool.Backend.Models;
using PSchool.Backend.Repositories;

namespace PSchool.Backend.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        UserRepository<User> Users { get; }
        IBaseRepository<Student> Students { get; }
        ParentRepository<Parent> Parents { get; }
        int Complete();

    }
}

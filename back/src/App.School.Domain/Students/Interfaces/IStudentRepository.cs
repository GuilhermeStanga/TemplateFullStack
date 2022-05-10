using App.School.Domain.IUoW;
using App.School.Domain.Students.Entities;

namespace App.School.Domain.Students.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsWithCpfAsync(string cpf, CancellationToken cancellationToken);
        Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}

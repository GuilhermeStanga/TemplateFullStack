using App.School.Domain.Students.Interfaces;

namespace App.School.Domain.IUoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        IStudentRepository StudentRepository { get; }
    }
}

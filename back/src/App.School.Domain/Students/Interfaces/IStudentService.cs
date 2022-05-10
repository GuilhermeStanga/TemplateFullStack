using App.School.Domain.Students.Entities;
using OperationResult;

namespace App.School.Domain.Students.Interfaces
{
    public interface IStudentService
    {
        Task<Status<string>> IsValid(Student student, CancellationToken cancellationToken);
    }
}

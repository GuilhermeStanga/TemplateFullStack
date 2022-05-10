using App.School.Core.Extensions;
using App.School.Domain.IUoW;
using App.School.Domain.Students.Entities;
using App.School.Domain.Students.Interfaces;
using OperationResult;
using static OperationResult.Helpers;

namespace App.School.Application.Students.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Status<string>> IsValid(Student student, CancellationToken cancellationToken)
        {
            if (!student.Cpf.IsValidCpf())
            {
                return Error("INVALID_CPF");
            }
            if (!student.Email.IsValidEmail())
            {
                return Error("INVALID_EMAIL");
            }
            if (await _uow.StudentRepository.ExistsWithCpfAsync(student.Cpf.NormaliseCpf(), cancellationToken))
            {
                return Error("CPF_ALREADY_REGISTERED");
            }

            return Ok();
        }
    }
}

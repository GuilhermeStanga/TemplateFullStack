using App.School.Application.Students.Commands;
using App.School.Application.Students.ViewModels;
using App.School.Core.Extensions;
using App.School.Domain.IUoW;
using App.School.Domain.Students.Entities;
using App.School.Domain.Students.Interfaces;
using MediatR;
using OperationResult;
using static OperationResult.Helpers;

namespace App.School.Application.Students.CommandHandlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<StudentViewModel, string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IStudentService _studentService;

        public CreateStudentCommandHandler(IUnitOfWork uow, IStudentService studentService)
        {
            _uow = uow;
            _studentService = studentService;
        }

        public async Task<Result<StudentViewModel, string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student();
            student.Name = request.Name;
            student.Email = request.Email;
            student.Cpf = request.Cpf.NormaliseCpf();

            var validate = await _studentService.IsValid(student, cancellationToken);
            if (validate.IsError)
            {
                return Error(validate.Error);
            }

            _uow.StudentRepository.Insert(student);
            await _uow.SaveChangesAsync(cancellationToken);

            var result = new StudentViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Cpf = student.Cpf
            };
            return Ok(result);
        }
    }
}

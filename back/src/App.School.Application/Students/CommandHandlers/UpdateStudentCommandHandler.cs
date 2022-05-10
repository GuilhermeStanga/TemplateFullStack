using App.School.Application.Students.Commands;
using App.School.Application.Students.ViewModels;
using App.School.Core.Extensions;
using App.School.Domain.IUoW;
using MediatR;
using OperationResult;
using static OperationResult.Helpers;

namespace App.School.Application.Students.CommandHandlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<StudentViewModel, string>>
    {
        private readonly IUnitOfWork _uow;

        public UpdateStudentCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<StudentViewModel, string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetByIdAsync(request.GetId(), cancellationToken);
            if (student == null)
            {
                return Error("STUDENT_NOT_FOUND");
            }
            if (!request.Email.IsValidEmail())
            {
                return Error("INVALID_EMAIL");
            }

            student.Name = request.Name;
            student.Email = request.Email;

            _uow.StudentRepository.Update(student);
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

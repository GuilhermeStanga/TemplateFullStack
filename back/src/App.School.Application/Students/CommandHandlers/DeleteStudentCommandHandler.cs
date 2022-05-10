using App.School.Application.Students.Commands;
using App.School.Application.Students.ViewModels;
using App.School.Core.Extensions;
using App.School.Domain.IUoW;
using MediatR;
using OperationResult;
using static OperationResult.Helpers;

namespace App.School.Application.Students.CommandHandlers
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Status<string>>
    {
        private readonly IUnitOfWork _uow;

        public DeleteStudentCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Status<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _uow.StudentRepository.GetByIdAsync(request.GetId(), cancellationToken);
            if (student == null)
            {
                return Error("STUDENT_NOT_FOUND");
            }
            student.IsDeleted = true;

            _uow.StudentRepository.Update(student);
            await _uow.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}

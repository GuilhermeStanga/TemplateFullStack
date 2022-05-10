using App.School.Application.Students.Queries;
using App.School.Application.Students.ViewModels;
using App.School.Core.Extensions;
using App.School.Domain.IUoW;
using App.School.Domain.Students.Entities;
using MediatR;
using OperationResult;
using static OperationResult.Helpers;

namespace App.School.Application.Students.QueriesHandlers
{
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, Result<List<StudentViewModel>, string>>
    {
        private readonly IUnitOfWork _uow;

        public GetStudentsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<StudentViewModel>, string>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Student> students;
            if (request.Name.IsNullOrEmpty())
            {
                students = await _uow.StudentRepository.GetAllAsync(cancellationToken);
            }
            else
            {
                students = await _uow.StudentRepository.GetAllByNameAsync(request.Name, cancellationToken);
            }

            List<StudentViewModel> result = new List<StudentViewModel>();
            foreach (var student in students)
            {
                var studentViewModel = new StudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Cpf = student.Cpf
                };
                result.Add(studentViewModel);
            }
            
            return Ok(result);
        }
    }
}

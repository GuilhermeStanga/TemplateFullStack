using App.School.Application.Students.ViewModels;
using MediatR;
using OperationResult;

namespace App.School.Application.Students.Queries
{
    public class GetStudentsQuery : IRequest<Result<List<StudentViewModel>, string>>
    {
        public string Name { get; set; }
        public GetStudentsQuery(string name)
        {
            Name = name;
        }
    }
}

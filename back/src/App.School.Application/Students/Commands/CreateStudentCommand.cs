using App.School.Application.Students.ViewModels;
using MediatR;
using OperationResult;

namespace App.School.Application.Students.Commands
{
    public class CreateStudentCommand : IRequest<Result<StudentViewModel, string>>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}

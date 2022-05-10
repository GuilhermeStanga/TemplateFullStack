using App.School.Application.Students.ViewModels;
using MediatR;
using OperationResult;

namespace App.School.Application.Students.Commands
{
    public class UpdateStudentCommand : IRequest<Result<StudentViewModel, string>>
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UpdateStudentCommand SetId(int id)
        {
            Id = id;
            return this;
        }

        public int GetId()
        {
            return Id;
        }
    }
}

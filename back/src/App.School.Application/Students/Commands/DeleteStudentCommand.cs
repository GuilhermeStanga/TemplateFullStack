using MediatR;
using OperationResult;

namespace App.School.Application.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Status<string>>
    {
        private int Id { get; set; }

        public DeleteStudentCommand(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }
    }
}

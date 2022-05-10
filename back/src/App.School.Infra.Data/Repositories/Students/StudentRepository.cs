using App.School.Domain.Students.Entities;
using App.School.Domain.Students.Interfaces;
using App.School.Infra.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace App.School.Infra.Data.Repositories.Students
{ 
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Student>> GetAllByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await Context.Students
                                .Where(s => s.IsDeleted == false)
                                .Where(s => s.Name.ToUpper().Contains(name.Trim().ToUpper()))
                                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Context.Students
                                .Where(s => s.IsDeleted == false)
                                .ToListAsync(cancellationToken);
        }

        public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Context.Students
                                .Where(s => s.IsDeleted == false && s.Id == id)
                                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsWithCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            return await Context.Students
                                .AsNoTracking()
                                .Where(s => s.IsDeleted == false && s.Cpf.Equals(cpf))
                                .AnyAsync(cancellationToken);
        }
    }
}

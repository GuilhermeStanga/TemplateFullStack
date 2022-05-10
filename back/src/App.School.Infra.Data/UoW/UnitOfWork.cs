using App.School.Domain.IUoW;
using App.School.Domain.Students.Interfaces;
using App.School.Infra.Data.Repositories.Students;

namespace App.School.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IStudentRepository _studentRepository;

        private readonly SchoolDbContext _context;
        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_context);
    }
}

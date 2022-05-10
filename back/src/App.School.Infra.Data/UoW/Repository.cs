using App.School.Domain.IUoW;

namespace App.School.Infra.Data.UoW
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected SchoolDbContext Context;

        public Repository(SchoolDbContext context)
        {
            Context = context;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public void Insert(T entity)
        {
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
        }
    }
}

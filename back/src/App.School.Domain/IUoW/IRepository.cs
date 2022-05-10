using System.Linq.Expressions;

namespace App.School.Domain.IUoW
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

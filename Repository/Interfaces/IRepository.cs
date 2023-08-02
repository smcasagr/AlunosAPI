using System.Linq.Expressions;

namespace AlunosAPI.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        Task<T> GetById(Expression<Func<T, bool>> predicate);

        Task<T> GetByNome(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}

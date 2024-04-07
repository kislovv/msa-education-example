using System.Linq.Expressions;

namespace ManagerAccount.UseCases.Abstractions.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T?> GetById(long id);
    Task<bool> Add(T entity);
    Task<bool> Delete(T id);
    Task<IEnumerable<T?>> Find(Expression<Func<T?, bool>> predicate);
}
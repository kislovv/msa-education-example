using System.Linq.Expressions;
using ManagerAccount.UseCases.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.Repositories.DataAccess.DbRepository;

public class GenericRepository<T>(
    AppDbContext context,
    ILogger logger) : IGenericRepository<T>
    where T : class
{
    protected readonly AppDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();
    public readonly ILogger Logger = logger;

    public virtual async Task<T?> GetById(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual  Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return Task.FromResult(true);
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        return await _dbSet.ToArrayAsync();
    }

    public async Task<IEnumerable<T?>> Find(Expression<Func<T?, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}
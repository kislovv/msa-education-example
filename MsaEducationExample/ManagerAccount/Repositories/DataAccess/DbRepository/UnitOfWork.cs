using ManagerAccount.UseCases.Abstractions.Repository;

namespace ManagerAccount.Repositories.DataAccess.DbRepository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;

    public IManagerRepository Managers { get; }
    
    public UnitOfWork(
        AppDbContext context,
        ILoggerFactory loggerFactory
    )
    {
        _context = context;
        Managers = new ManagerRepository(_context, loggerFactory.CreateLogger<ManagerRepository>());
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose() => _context.Dispose();
}
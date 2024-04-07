using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.Repositories.DataAccess.DbRepository;

public class ManagerRepository(AppDbContext appDbContext) : IManagerRepository
{
    public async Task<Manager> Create(Manager manager)
    {
        var result = await appDbContext.Managers.AddAsync(manager);
        return result.Entity;
    }

    public async Task<Manager?> GetById(long id)
    {
        return await appDbContext.Managers
            .Include(m => m.OrderIds)
            .SingleOrDefaultAsync(m => m.Id == id);
    }

    public void Update(Manager manager)
    {
        appDbContext.Managers.Update(manager);
    }
}
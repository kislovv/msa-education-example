using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Abstractions.Repository;
using ManagerAccount.UseCases.Entities;
using ManagerAccount.UseCases.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.Repositories.DataAccess.DbRepository;

public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
{
    internal ManagerRepository(AppDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<Manager?> GetById(long id)
    {
        return await Context.Managers.SingleOrDefaultAsync(m => m.Id == id);
    }

    public void Update(Manager manager)
    {
        throw new NotImplementedException();
    }
}
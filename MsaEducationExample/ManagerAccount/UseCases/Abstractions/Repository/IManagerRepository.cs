using ManagerAccount.UseCases.Entities.Models;

namespace ManagerAccount.UseCases.Abstractions.Repository;

public interface IManagerRepository : IGenericRepository<Manager>
{
    void Update(Manager manager);
}
using ManagerAccount.UseCases.Entities;

namespace ManagerAccount.UseCases.Abstractions;

public interface IManagerRepository
{
    Task<Manager> Create(Manager manager);
    Task<Manager?> GetById(long id);
    void Update(Manager manager);
}
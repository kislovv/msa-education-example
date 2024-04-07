namespace ManagerAccount.UseCases.Abstractions.Repository;

public interface IUnitOfWork
{
    IManagerRepository Managers { get; } 
    Task CompleteAsync();
}
namespace ManagerAccount.UseCases.Entities.Models;

public class Manager: User
{
    public string FullName { get; set; }
    public List<OrderToManager> OrderIds { get; set; }
}
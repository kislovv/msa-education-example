namespace ManagerAccount.UseCases.Entities.Models;

public class OrderToManager
{
    public long Id { get; set; }
    public Manager Manager { get; set; }
}
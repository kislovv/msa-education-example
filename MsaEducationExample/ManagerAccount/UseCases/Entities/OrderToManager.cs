namespace ManagerAccount.UseCases.Entities;

public class OrderToManager
{
    public long Id { get; set; }
    public Manager Manager { get; set; }
}
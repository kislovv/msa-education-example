namespace ManagerAccount.Repositories.Frameworks.HubIntegrations;

public class CreateOrderDetails
{
    /// <summary>
    /// Тип доставляемого груза
    /// </summary>
    public TypeOfProduct TypeOfProduct { get; set; }
    /// <summary>
    /// Объем груза
    /// </summary>
    public decimal Value { get; set; }
}
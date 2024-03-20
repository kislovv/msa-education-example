namespace ManagerAccount.Models.Responses;

public class PrepareOrderResponse
{
    public TypeOfProduct TypeOfProduct { get; set; }
    public decimal Value { get; set; }
    public OrderStatus Status { get; set; }
}
namespace ManagerAccount.UseCases.Dtos;

public class OrderDto
{
    public long? Id { get; set; }
    public long ManagerId { get; set; }
    public string ClientEmail { get; set; }
    public OrderDetailsDto OrderDetails { get; set; }
    public OrderStatusDto OrderStatus { get; set; }
}
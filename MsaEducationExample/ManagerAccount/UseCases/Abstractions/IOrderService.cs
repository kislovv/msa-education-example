using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.UseCases.Abstractions;

public interface IOrderService
{
    Task<Result<OrderDto>> CreateOrder(OrderDto orderDto);
}
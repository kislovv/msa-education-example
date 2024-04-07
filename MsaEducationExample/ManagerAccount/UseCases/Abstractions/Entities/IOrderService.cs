using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.UseCases.Abstractions.Entities;

public interface IOrderService
{
    Task<Result<OrderDto>> CreateOrder(OrderDto orderDto);
}
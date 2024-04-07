using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.UseCases.Abstractions;

public interface IHubService
{
    Task<Result<OrderDto>> CreateOrder(OrderDto orderDto);
}
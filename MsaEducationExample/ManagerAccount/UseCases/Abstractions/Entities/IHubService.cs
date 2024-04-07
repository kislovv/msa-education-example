using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.UseCases.Abstractions.Entities;

public interface IHubService
{
    Task<Result<OrderDto>> CreateOrder(OrderDto orderDto);
}
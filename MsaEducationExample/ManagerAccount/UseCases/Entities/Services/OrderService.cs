using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.UseCases.Entities.Services;

public class OrderService(IHubService hubService, IManagerRepository managerRepository) : IOrderService
{
    public async Task<Result<OrderDto>> CreateOrder(OrderDto orderDto)
    {
        var manager = await managerRepository.GetById(orderDto.ManagerId);
        if (manager is null)
        {
            return new Result<OrderDto>()
            {
                Error = "Manager Not Found!",
                ErrorCode = 404
            };
        }
        var result = await hubService.CreateOrder(orderDto);

        if (!result.IsSuccess)
        {
            return result;
        }

        manager.OrderIds.Add(new OrderToManager
        {
            Id = (long)result.Data.Id!
        });
        managerRepository.Update(manager);

        return result;
    }
}
using ManagerAccount.UseCases.Abstractions.Entities;
using ManagerAccount.UseCases.Abstractions.Repository;
using ManagerAccount.UseCases.Dtos;
using ManagerAccount.UseCases.Entities.Models;

namespace ManagerAccount.UseCases.Entities.Services;

public class OrderService(IHubService hubService, IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Result<OrderDto>> CreateOrder(OrderDto orderDto)
    {
        var manager = await unitOfWork.Managers.GetById(orderDto.ManagerId);
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
        unitOfWork.Managers.Update(manager);
        
        await unitOfWork.CompleteAsync();

        return result;
    }
}
using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Abstractions.Entities;
using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.Repositories.Frameworks.HubIntegrations;

public class HubService(HttpClient httpClient): IHubService
{
    public async Task<Result<OrderDto>> CreateOrder(OrderDto orderDto)
    {
        var response = await httpClient.PostAsJsonAsync("/order/create", new CreateOrderRequest());

        if (!response.IsSuccessStatusCode)
            return new Result<OrderDto>()
            {
                Error = $"Что то пошло не так в сервисе Hub: {response.ReasonPhrase}",
                ErrorCode = 10001
            };
        
        var result = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
        orderDto.OrderStatus = (OrderStatusDto)result!.Status;
        orderDto.Id = result.Id;
            
        return new Result<OrderDto>
        {
            Data = orderDto,
            IsSuccess = true
        };
    }
}
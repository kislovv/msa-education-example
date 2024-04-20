using LogisticHub.Abstraction;
using LogisticHub.Database;
using LogisticHub.Entities;
using LogisticHub.Models;
using MediatR;
using MsaSharedContracts;

namespace LogisticHub.Handlers;

public class CreateOrderCommandHandler(AppDbContext appDbContext, IPushService pushService) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var freeTanker = appDbContext.Tankers.FirstOrDefault(tanker => tanker.FreeVolume >= request.Value);
        var createdAt = DateTimeOffset.UtcNow;
        
        if (freeTanker == null)
        {
            await pushService.SendMessage(new OrderNotificationDto
            {
                Status = OrderStatus.Rejected.ToString(),
                UpdatedAt = DateTimeOffset.UtcNow,
                EmailContract = request.ClientEmail,
                IsTerminated = true,
                //TODO: change nullable
                OrderId = 0
            });
            
            return new Order
            {
                Status = OrderStatus.Rejected,
                Type = request.Type,
                Value = request.Value,
                ClientEmail = request.ClientEmail,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }

        var response = await appDbContext.Orders.AddAsync(new Order
        {
            Status = OrderStatus.Created,
            Type = request.Type,
            Value = request.Value,
            ClientEmail = request.ClientEmail,
            Tanker = freeTanker,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        }, cancellationToken);

        await appDbContext.SaveChangesAsync(cancellationToken);
        
        await pushService.SendMessage(new OrderNotificationDto
        {
            Status = response.Entity.Status.ToString(),
            UpdatedAt = response.Entity.CreatedAt,
            EmailContract = request.ClientEmail,
            IsTerminated = false,
            OrderId = response.Entity.Id
        });
        
        return response.Entity;
    }
}
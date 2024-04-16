using LogisticHub.Database;
using LogisticHub.Entities;
using LogisticHub.Models;
using MediatR;

namespace LogisticHub.Handlers;

public class CreateOrderCommandHandler(AppDbContext appDbContext) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var freeTanker = appDbContext.Tankers.FirstOrDefault(tanker => tanker.FreeVolume >= request.Value);
        var createdAt = DateTimeOffset.UtcNow;
        if (freeTanker == null)
        {
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
        
        return response.Entity;
    }
}
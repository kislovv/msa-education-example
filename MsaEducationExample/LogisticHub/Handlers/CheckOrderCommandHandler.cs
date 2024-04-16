using LogisticHub.Database;
using LogisticHub.Entities;
using LogisticHub.Models;
using MediatR;

namespace LogisticHub.Handlers;

public class CheckOrderCommandHandler(AppDbContext dbContext) : IRequestHandler<CheckOrderCommand>
{
    public async Task Handle(CheckOrderCommand request, CancellationToken cancellationToken)
    {
        var expiredOrders = dbContext!.Orders.Where(or => 
            or.Status != OrderStatus.Closed || or.Status != OrderStatus.Completed ||
            or.Status != OrderStatus.Expired || or.Status != OrderStatus.Rejected && 
            DateTimeOffset.UtcNow.Day - or.CreatedAt.Day > 3).ToList();

        if (expiredOrders.Count == 0)
        {
            return;
        }
            
        expiredOrders.ForEach(or =>
        {
            or.Status = OrderStatus.Expired;
        });
        dbContext.Orders.UpdateRange(expiredOrders);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
using Hangfire;
using LogisticHub.Models;
using MediatR;

namespace LogisticHub.HostedServices;

public class CheckExpiredOrdersHandlerService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
    
        var mediator = scope.ServiceProvider.GetService<IMediator>();
        RecurringJob.AddOrUpdate("name",
            () => mediator!.Send(new CheckOrderCommand(), stoppingToken),
            "00 00 * * *");
    }
}
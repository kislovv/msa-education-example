using LogisticHub.Database;
using Microsoft.EntityFrameworkCore;

namespace LogisticHub.HostedServices;

public class MigrationHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        
        await dbContext!.Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
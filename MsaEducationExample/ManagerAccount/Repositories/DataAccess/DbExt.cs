using ManagerAccount.Repositories.DataAccess.DbRepository;
using ManagerAccount.UseCases.Abstractions;
using ManagerAccount.UseCases.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.Repositories.DataAccess;

public static class DbExt
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        return serviceCollection.AddDbContext<AppDbContext>(builder =>
        {
            builder.UseNpgsql(configuration["Database:ConnectionString"]);
            builder.UseSnakeCaseNamingConvention();
        });
    }
}
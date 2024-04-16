using Carter;
using Hangfire;
using Hangfire.PostgreSql;
using LogisticHub.Database;
using LogisticHub.Handlers;
using LogisticHub.HostedServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddHangfire(configuration =>
{
    configuration.UsePostgreSqlStorage(options =>
    {
        options.UseNpgsqlConnection(builder.Configuration["Database:Hangfire"]);
    });
});
builder.Services.AddHangfireServer();
builder.Services.AddHostedService<MigrationHostedService>();
builder.Services.AddHostedService<CheckExpiredOrdersHandlerService>();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration["Database:ConnectionString"]);
    option.UseSnakeCaseNamingConvention();
});

var app = builder.Build();

app.UseHangfireDashboard();
app.MapCarter();
app.MapGet("/", () => "Hello World!");
app.Run();
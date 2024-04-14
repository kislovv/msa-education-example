using Carter;
using LogisticHub.Database;
using LogisticHub.Handlers;
using LogisticHub.HostedServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddHostedService<MigrationHostedService>();
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

app.MapCarter();
app.MapGet("/", () => "Hello World!");
app.Run();
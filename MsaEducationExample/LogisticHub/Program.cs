using Carter;
using LogisticHub.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
});
var app = builder.Build();

app.MapCarter();
app.MapGet("/", () => "Hello World!");
app.Run();
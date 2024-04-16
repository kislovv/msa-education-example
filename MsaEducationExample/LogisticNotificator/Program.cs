using LogisticNotificator.Configurations;
using LogisticNotificator.HostedServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<RabbitMqListener>();
var app = builder.Build();

app.Run();
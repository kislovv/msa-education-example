using LogisticNotificator.Abstractions;
using LogisticNotificator.Configurations;
using LogisticNotificator.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<MailConfig>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
    configurator.AddConsumer<OrderConsumer>(consumerConfigurator =>
    {
    });
});
builder.Services.AddScoped<IMailSender, MailSender>();

var app = builder.Build();

app.Run();
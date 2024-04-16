using System.Text;
using System.Text.Json;
using LogisticHub.Abstraction;
using LogisticHub.Configurations;
using Microsoft.Extensions.Options;
using MsaSharedContracts;
using RabbitMQ.Client;

namespace LogisticHub.Services;

public class PushService(IOptionsMonitor<RabbitMqConfig> optionsMonitor) : IPushService
{
    private readonly RabbitMqConfig _rabbitMqConfig = optionsMonitor.CurrentValue;
    public Task SendMessage(OrderNotificationDto dto)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.Host,
            Port = _rabbitMqConfig.Port,
            UserName = _rabbitMqConfig.Username,
            Password = _rabbitMqConfig.Password
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(ExchangeNames.Notification, ExchangeType.Fanout);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto));

        channel.BasicPublish(ExchangeNames.Notification,
            routingKey: "",
            basicProperties: null,
            body: body);
        
        return Task.CompletedTask;
    }
}
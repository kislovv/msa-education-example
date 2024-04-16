using System.Diagnostics;
using System.Text;
using System.Text.Json;
using LogisticNotificator.Configurations;
using Microsoft.Extensions.Options;
using MsaSharedContracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogisticNotificator.HostedServices;

public class RabbitMqListener(IOptionsMonitor<RabbitMqConfig> rabbitMqConfigOptions) : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private RabbitMqConfig _rabbitMqConfig = rabbitMqConfigOptions.CurrentValue;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        stoppingToken.ThrowIfCancellationRequested();
        
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.Host,
            Port = _rabbitMqConfig.Port,
            UserName = _rabbitMqConfig.Username,
            Password = _rabbitMqConfig.Password
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(ExchangeNames.Notification, ExchangeType.Fanout, true, false);
        _channel.QueueDeclare(queue: QueueNames.NotificationQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(QueueNames.NotificationQueue, ExchangeNames.Notification, "");
        

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var memoryStream = new MemoryStream(ea.Body.ToArray());
            var notification = await JsonSerializer.DeserializeAsync<OrderNotificationDto>(memoryStream, cancellationToken: stoppingToken);
			
            //TODO: Add smtp service
            Debug.WriteLine($"Получено сообщение: {notification!.Status}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(QueueNames.NotificationQueue, false, consumer);

        return Task.CompletedTask;
    }
	
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
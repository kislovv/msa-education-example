using LogisticHub.Abstraction;
using MassTransit;
using MsaSharedContracts;

namespace LogisticHub.Services;

public class PushService(IPublishEndpoint publishEndpoint) : IPushService
{
    public async Task SendMessage(OrderNotificationDto dto)
    {
        await publishEndpoint.Publish(dto);
    }
}
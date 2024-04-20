using LogisticNotificator.Abstractions;
using MassTransit;
using MsaSharedContracts;

namespace LogisticNotificator.Services;

public class OrderConsumer(IMailSender sender) : IConsumer<OrderNotificationDto>
{
    public async Task Consume(ConsumeContext<OrderNotificationDto> context)
    {
        await sender.SendMail(context.Message);
    }
}
using Logistic.Status.Proto;
using LogisticNotificator.Abstractions;
using MassTransit;
using MsaSharedContracts;
using TerminateStatusServiceClient = Logistic.Status.Proto.TerminateStatusService.TerminateStatusServiceClient;

namespace LogisticNotificator.Services;

public class OrderConsumer(IMailSender sender, TerminateStatusServiceClient terminateClient) : IConsumer<OrderNotificationDto>
{
    public async Task Consume(ConsumeContext<OrderNotificationDto> context)
    {
        await sender.SendMail(context.Message);

        if (context.Message.Status == "Completed")
        {
            _ = await terminateClient.SendTerminateStatusAsync(new SendStatusRequest
            {
                Message = "Заявка успешно завершена",
                OrderId = context.Message.OrderId,
                OrderStatus = OrderStatus.Completed
            });
        }
    }
}
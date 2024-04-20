using MsaSharedContracts;

namespace LogisticNotificator.Abstractions;

public interface IMailSender
{
    Task SendMail(OrderNotificationDto orderDto);
}
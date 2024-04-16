using MsaSharedContracts;

namespace LogisticHub.Abstraction;

public interface IPushService
{
    Task SendMessage(OrderNotificationDto dto);
}
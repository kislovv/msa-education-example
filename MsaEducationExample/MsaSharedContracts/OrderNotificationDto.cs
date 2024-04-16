namespace MsaSharedContracts;

public class OrderNotificationDto
{
    public long OrderId { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string EmailContract { get; set; }
    //TODO: add enum
    public string Status { get; set; }
    public bool IsTerminated { get; set; }
}
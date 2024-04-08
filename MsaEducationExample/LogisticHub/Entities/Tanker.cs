namespace LogisticHub.Entities;

public class Tanker
{
    public long Id { get; set; }
    public decimal FreeVolume { get; set; }
    public List<Order> Orders { get; set; } = [];
}
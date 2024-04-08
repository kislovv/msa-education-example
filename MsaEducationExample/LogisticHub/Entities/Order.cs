using System.Security.Cryptography;

namespace LogisticHub.Entities;

public class Order
{
    public long Id { get; set; }
    public string ClientEmail { get; set; }
    public TypeOfProduct Type { get; set; }
    public decimal Value { get; set; }
    public OrderStatus Status { get; set; }
    public Tanker? Tanker { get; set; }
}
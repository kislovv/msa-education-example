using System.Security.Cryptography;

namespace LogisticHub.Entities;

public class Order
{
    public long Id { get; set; }
    public TypeOfProduct Type { get; set; }
}
using LogisticHub.Entities;
using MediatR;

namespace LogisticHub.Models;

public class CreateOrderCommand : IRequest<Order>
{
    public required string ClientEmail { get; set; }
    public TypeOfProduct Type { get; set; }
    public decimal Value { get; set; }
    public OrderStatus Status { get; set; }
}
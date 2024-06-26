﻿using ManagerAccount.Models;

namespace ManagerAccount.Presenter.Models.Responses;

public class PrepareOrderResponse
{
    public long? Id { get; set; }
    public TypeOfProduct TypeOfProduct { get; set; }
    public decimal Value { get; set; }
    public OrderStatus Status { get; set; }
    public long ManagerId { get; set; }
}
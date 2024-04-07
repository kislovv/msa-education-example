#define Models

using ManagerAccount.Models;

namespace ManagerAccount.Presenter.Models.Requests;

/// <summary>
/// Детали заявки
/// </summary>
public class OrderDetails
{
    /// <summary>
    /// Тип доставляемого груза
    /// </summary>
    public TypeOfProduct TypeOfProduct { get; set; }
    /// <summary>
    /// Объем груза
    /// </summary>
    public decimal Value { get; set; }
}
using ManagerAccount.Models.Requests;

namespace ManagerAccount.Presenter.Models.Requests;

/// <summary>
/// Запрос на создание заявки на доставку груза
/// </summary>
public class OrderRequest
{
    /// <summary>
    /// Почта клиента
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Детали заявки
    /// </summary>
    public OrderDetails OrderDetails { get; set; }
}
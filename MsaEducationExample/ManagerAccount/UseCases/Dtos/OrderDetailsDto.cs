namespace ManagerAccount.UseCases.Dtos;

/// <summary>
/// Детали заявки
/// </summary>
public class OrderDetailsDto
{
    /// <summary>
    /// Тип доставляемого груза
    /// </summary>
    public TypeOfProductDto TypeOfProduct { get; set; }
    /// <summary>
    /// Объем груза
    /// </summary>
    public decimal Value { get; set; }
}
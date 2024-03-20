#if Models
namespace ManagerAccount.Models; 
#else
namespace LogisticHub.Entities;
#endif
/// <summary>
/// Тип доставляемого груза
/// </summary>
public enum TypeOfProduct
{
    /// <summary>
    /// Масло
    /// </summary>
    Oil = 1,
    
    /// <summary>
    /// Бензин
    /// </summary>
    Petrol = 2,
    
    /// <summary>
    /// Газ
    /// </summary>
    Gas = 3
}
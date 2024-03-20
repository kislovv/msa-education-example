#if Models
namespace ManagerAccount.Models; 
#else
namespace LogisticHub.Entities;
#endif
public enum OrderStatus
{
    Prepared,
    Created,
    InProgress,
    Rejected,
    Completed,
    Closed
}

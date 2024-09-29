
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBankService
    {
        Task Pay(Transaction transaction);
        Task Cancel(Transaction transaction);
        Task Refund(Transaction transaction, decimal amount);
    }
}
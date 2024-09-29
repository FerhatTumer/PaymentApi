using Core.Entities;
namespace Core.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<Transaction> GetByIdWithDetailAsync(Guid id);
    }
}
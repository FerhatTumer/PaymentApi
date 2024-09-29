using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(PaymentDbContext context) : base(context)
        {

        }
        public async Task<Transaction> GetByIdWithDetailAsync(Guid id)
        {
            return await _context.Transaction
                   .Include(t => t.TransactionDetails)
                   .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
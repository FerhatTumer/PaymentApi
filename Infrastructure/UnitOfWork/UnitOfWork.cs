using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _context;
        public UnitOfWork(PaymentDbContext context)
        {
            _context = context;
            TransactionRepository = new TransactionRepository(_context);
        }
        public ITransactionRepository TransactionRepository { get; }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
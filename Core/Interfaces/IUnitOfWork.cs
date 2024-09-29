namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        ITransactionDetailRepository TransactionDetailRepository { get; }

        Task<int> CommitAsync();
    }
}
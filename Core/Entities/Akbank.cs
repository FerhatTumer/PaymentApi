namespace Core.Entities
{
    public class Akbank : BaseBank
    {
        public override Task Pay(Transaction transaction)
        {
            transaction.MarkAsSuccess();
            return Task.CompletedTask;
        }
        public override Task Cancel(Transaction transaction)
        {
            if (transaction.TransactionDate.Date == DateTime.UtcNow.Date)
            {
                transaction.MarkAsCancelled();
            }
            else
            {
                throw new InvalidOperationException("Cancellation can only be made on the same day.");
            }
            return Task.CompletedTask;
        }
        public override Task Refund(Transaction transaction, decimal amount)
        {
            if (transaction.TransactionDate.Date < DateTime.UtcNow.Date)
            {
                transaction.MarkAsRefunded(amount);
            }
            else
            {
                throw new InvalidOperationException("Refund can only be made after one day.");
            }
            return Task.CompletedTask;
        }
    }
}
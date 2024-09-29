
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class TransactionDetail
    {
        public Guid Id { get; }
        public Guid TransactionId { get; }
        public string TransactionType { get; }
        public decimal Amount { get; }
        public string Status { get; private set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        // Private constructor for immutability
        private TransactionDetail() { }
        // Public constructor for creating a detail
        public TransactionDetail(Guid transactionId, string transactionType, decimal amount)
        {
            Id = Guid.NewGuid();
            TransactionId = transactionId;
            TransactionType = transactionType;
            Amount = amount;
            Status = TransactionStatus.Pending.ToString();
        }
        // Mark this transaction detail as successful
        public void MarkAsSuccess()
        {
            EnsureIsPending();
            Status = TransactionStatus.Success.ToString();
        }
        // Mark this transaction detail as failed
        public void MarkAsFailed()
        {
            Status = TransactionStatus.Failed.ToString();
        }
        // Helper method to enforce the pending status rule
        private void EnsureIsPending()
        {
            if (Status != TransactionStatus.Pending.ToString())
            {
                throw new InvalidOperationException("Transaction detail is already processed.");
            }
        }
    }
}
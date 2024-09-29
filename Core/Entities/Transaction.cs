using Core.Enums;
using Core.Events;

namespace Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; }
        public string BankId { get; }
        public decimal TotalAmount { get; }
        public decimal NetAmount { get; private set; }
        public string Status { get; private set; }
        public string OrderReference { get; }
        public DateTime TransactionDate { get; }
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        private readonly List<TransactionDetail> _transactionDetails = new();
        public IReadOnlyCollection<TransactionDetail> TransactionDetails => _transactionDetails.AsReadOnly();
        // Private constructor to enforce immutability on creation
        private Transaction() { }
        // Constructor that defines an initial transaction state
        public Transaction(string bankId, decimal totalAmount, string orderReference)
        {
            Id = Guid.NewGuid();
            BankId = bankId;
            TotalAmount = totalAmount;
            NetAmount = totalAmount;
            Status = TransactionStatus.Pending.ToString();
            OrderReference = orderReference;
            TransactionDate = DateTime.UtcNow;
            // Automatically add initial transaction detail
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Pending.ToString(), totalAmount));
        }
        // Mark transaction as successful and update its details
        public void MarkAsCancelled()
        {
            EnsureTransactionIsSuccess();
            Status = TransactionStatus.Cancelled.ToString();
            CancelAmount();
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Cancel.ToString(), TotalAmount));
            // Raise the domain event
            _domainEvents.Add(new TransactionCancelledEvent(Id));
        }
        public void MarkAsSuccess()
        {
            EnsureTransactionIsPending();
            Status = TransactionStatus.Success.ToString();
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Sale.ToString(), TotalAmount));
            // Raise the domain event
            _domainEvents.Add(new TransactionSucceededEvent(Id));
        }
        public void MarkAsRefunded(decimal amount)
        {
            EnsureTransactionIsSuccess();
            EnsureRefundAmountIsValid(amount);
            Status = TransactionStatus.Refunded.ToString();
            RefundAmount(amount);
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Refund.ToString(), amount));
            // Raise the domain event
            _domainEvents.Add(new TransactionRefundedEvent(Id, amount));
        }
        // Method to clear events after they have been dispatched
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        // Helper methods to enforce business rules
        private void EnsureTransactionIsPending()
        {
            if (Status != TransactionStatus.Pending.ToString())
            {
                throw new InvalidOperationException("Transaction is already processed.");
            }
        }
        private void EnsureTransactionIsSuccess()
        {
            if (Status != TransactionStatus.Success.ToString())
            {
                throw new InvalidOperationException("Only successful transactions can be cancelled or refunded.");
            }
        }
        private void EnsureRefundAmountIsValid(decimal amount)
        {
            if (amount > NetAmount)
            {
                throw new InvalidOperationException("Refund amount cannot be greater than the net amount.");
            }
        }
        private void CancelAmount()
        {
            NetAmount = 0;
        }
        private void RefundAmount(decimal amount)
        {
            NetAmount -= amount;
        }
        // Add transaction detail
        private void AddTransactionDetail(TransactionDetail detail)
        {
            if (detail.TransactionId != Id)
            {
                throw new InvalidOperationException("Transaction detail does not belong to this transaction.");
            }
            _transactionDetails.Add(detail);
        }
    }
}
namespace Core.Events
{
    public class TransactionCancelledEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public decimal Amount { get; set; }
        public TransactionCancelledEvent(Guid transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
    public class TransactionSucceededEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public decimal Amount { get; set; }

        public TransactionSucceededEvent(Guid transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
    public class TransactionRefundedEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public decimal RefundedAmount { get; }
        public TransactionRefundedEvent(Guid transactionId, decimal refundedAmount)
        {
            TransactionId = transactionId;
            RefundedAmount = refundedAmount;
        }
    }
}
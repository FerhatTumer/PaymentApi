namespace Core.Events
{
    public class TransactionCancelledEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public TransactionCancelledEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
    public class TransactionSucceededEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public TransactionSucceededEvent(Guid transactionId)
        {
            TransactionId = transactionId;
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
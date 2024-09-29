namespace Core.Enums
{
    public enum TransactionStatus
    {
        Pending = 1,  // Transaction is created and waiting for processing
        Success = 2,  // Transaction completed successfully
        Cancelled = 3, // Transaction was cancelled
        Refunded = 4,  // Transaction was refunded
        Failed = 5     // Transaction failed during processing
    }
}
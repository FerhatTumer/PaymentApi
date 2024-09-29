namespace Core.Enums
{
    public enum TransactionType
    {
        Pending = 1,   // Initial pending state
        Sale = 2,      // Transaction involves a sale/purchase
        Cancel = 3,    // Transaction cancellation
        Refund = 4     // Transaction refund
    }
}
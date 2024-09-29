namespace Core.Entities
{
    public class TransactionDetail
    {
        public Guid Id { get; }
        public Guid TransactionId { get; }
        public string TransactionType { get; }
        public decimal Amount { get; }
        public string Status { get; private set; }
        public Transaction Transaction { get; set; }
        public TransactionDetail(){}
        public TransactionDetail(Guid transactionId, string transactionType, decimal amount)
        {
            Id = Guid.NewGuid();
            TransactionId = transactionId;
            TransactionType = transactionType;
            Amount = amount;
            Status = "Pending";
        }
        public void MarkAsSuccess()
        {
            if (Status != "Pending")
                throw new InvalidOperationException("Transaction detail is already processed.");
            Status = "Success";
        }
        public void MarkAsFailed()
        {
            Status = "Failed";
        }
    }
}
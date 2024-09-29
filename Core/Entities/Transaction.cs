using Core.Enums;

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

        private readonly List<TransactionDetail> _transactionDetail = new();
        public IReadOnlyCollection<TransactionDetail> TransactionDetail => _transactionDetail.AsReadOnly();
        public Transaction() { }
        public Transaction(string bankId, decimal totalAmount, string orderReference)
        {
            Id = Guid.NewGuid();
            BankId = bankId;
            TotalAmount = totalAmount;
            NetAmount = totalAmount;
            Status = "Pending";
            OrderReference = orderReference;
            TransactionDate = DateTime.Now;
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Pending.ToString(), TotalAmount));
        }
        public void MarkAsSuccess()
        {
            if (Status != "Pending")
                throw new InvalidOperationException("Transaction is already processed.");
            Status = "Success";
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Sale.ToString(), TotalAmount));
        }
        public void MarkAsCancelled()
        {
            if (Status != "Success")
                throw new InvalidOperationException("Only successful transactions can be cancelled.");
            Status = "Cancelled";
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Cancel.ToString(), TotalAmount));
        }
        public void MarkAsRefunded()
        {
            if (Status != "Success")
                throw new InvalidOperationException("Only successful transactions can be refunded.");
            Status = "Refunded";
            AddTransactionDetail(new TransactionDetail(Id, TransactionType.Refund.ToString(), TotalAmount));
        }
        public void UpdateNetAmount(decimal amount)
        {
            NetAmount -= amount;
        }
        public void CancelAmount()
        {
            NetAmount = 0;
        }
        public void RefundAmount(decimal amount)
        {
            if (amount > NetAmount)
                throw new InvalidOperationException("Refund amount can not be bigger than net amount");

            NetAmount -= amount;

        }
        public void AddTransactionDetail(TransactionDetail detail)
        {
            if (detail.TransactionId != Id)
                throw new InvalidOperationException("Transaction detail does not belong to this transaction.");
            _transactionDetail.Add(detail);
        }
    }
}
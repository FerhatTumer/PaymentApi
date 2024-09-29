namespace Application.DTOs
{
    public record RefundTransactionDto(
        Guid TransactionId,
        decimal Amount
    );
}
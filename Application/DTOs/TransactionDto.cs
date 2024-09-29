namespace Application.DTOs
{
    public record TransactionDto(
        Guid Id,
        string BankId,
        decimal TotalAmount,
        decimal NetAmount,
        string Status,
        string OrderReference,
        DateTime TransactionDate
    );
}
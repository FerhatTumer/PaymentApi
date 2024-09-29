namespace Application.DTOs
{
    public record CreateTransactionDto(
        string BankId,
        decimal TotalAmount,
        string OrderReference
    );
}
using Application.DTOs;
using FluentValidation;
namespace Application.Validators
{
    public class RefundTransactionDtoValidator : AbstractValidator<RefundTransactionDto>
    {
        public RefundTransactionDtoValidator()
        {
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required.");
        }
    }
}
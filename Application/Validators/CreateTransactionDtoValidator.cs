using Application.DTOs;
using FluentValidation;
namespace Application.Validators
{
    public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionDtoValidator()
        {
            RuleFor(x => x.BankId)
                .NotEmpty().WithMessage("BankId is required.");
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("TotalAmount must be greater than 0.");
            RuleFor(x => x.OrderReference)
                .NotEmpty().WithMessage("OrderReference is required.")
                .MaximumLength(50).WithMessage("OrderReference cannot be longer than 50 characters.");
        }
    }
}
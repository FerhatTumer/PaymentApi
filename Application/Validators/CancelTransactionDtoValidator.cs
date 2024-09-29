using Application.DTOs;
using FluentValidation;
namespace Application.Validators
{
   public class CancelTransactionDtoValidator : AbstractValidator<CancelTransactionDto>
   {
       public CancelTransactionDtoValidator()
       {
           RuleFor(x => x.TransactionId)
               .NotEmpty().WithMessage("TransactionId is required.");
       }
   }
}
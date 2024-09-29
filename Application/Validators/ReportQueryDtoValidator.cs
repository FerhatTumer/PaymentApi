using Application.DTOs;
using FluentValidation;
namespace Application.Validators
{
    public class ReportQueryDtoValidator : AbstractValidator<ReportQueryDto>
    {
        public ReportQueryDtoValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("StartDate must be less than or equal to EndDate.");
            RuleFor(x => x.Status)
                .Must(status => string.IsNullOrEmpty(status) || status == "Success" || status == "Fail")
                .WithMessage("Status must be either 'Success', 'Fail' or empty.");
        }
    }
}
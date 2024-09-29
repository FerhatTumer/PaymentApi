using System;
namespace Application.DTOs
{
    public record ReportQueryDto(
        string BankId,
        string Status,
        string OrderReference,
        DateTime? StartDate,
        DateTime? EndDate
    );
}
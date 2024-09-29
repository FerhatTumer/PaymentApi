using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
namespace Application.Queries
{
    public record ReportQuery(
        string? BankId,
        string Status,
        string OrderReference,
        DateTime? StartDate,
        DateTime? EndDate
    ) : IRequest<IEnumerable<Transaction>>;
}
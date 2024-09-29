using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Handlers
{
    public class ReportQueryHandler : IRequestHandler<ReportQuery, IEnumerable<Transaction>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Transaction>> Handle(ReportQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.TransactionRepository.AsQueryable();

            if (!string.IsNullOrEmpty(request.BankId))
            {
                query = query.Where(t => t.BankId == request.BankId);
            }
            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(t => t.Status.Equals(request.Status, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.OrderReference))
            {
                query = query.Where(t => t.OrderReference.Equals(request.OrderReference, StringComparison.OrdinalIgnoreCase));
            }
            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= request.StartDate.Value && t.TransactionDate <= request.EndDate.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using MediatR;
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
            var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.BankId))
            {
                transactions = transactions.Where(t => t.BankId == request.BankId);
            }
            if (!string.IsNullOrEmpty(request.Status))
            {
                transactions = transactions.Where(t => t.Status.Equals(request.Status, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.OrderReference))
            {
                transactions = transactions.Where(t => t.OrderReference.Equals(request.OrderReference, StringComparison.OrdinalIgnoreCase));
            }
            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                transactions = transactions.Where(t => t.TransactionDate >= request.StartDate.Value && t.TransactionDate <= request.EndDate.Value);
            }
            return transactions;
        }
    }
}
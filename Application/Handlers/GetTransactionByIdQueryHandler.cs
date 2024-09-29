using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Handlers
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Transaction>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Transaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                throw new Exception($"Transaction with ID {request.TransactionId} not found.");
            }
            return transaction;
        }
    }
}
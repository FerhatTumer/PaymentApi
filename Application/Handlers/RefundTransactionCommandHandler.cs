using Application.Commands;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Handlers
{
    public class RefundTransactionCommandHandler : IRequestHandler<RefundTransactionCommand, Transaction>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankService _bankService;
        public RefundTransactionCommandHandler(IUnitOfWork unitOfWork, IBankService bankService)
        {
            _unitOfWork = unitOfWork;
            _bankService = bankService;
        }
        public async Task<Transaction> Handle(RefundTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
                throw new Exception("Transaction not found.");
            await _bankService.Refund(transaction, request.Amount);
            await _unitOfWork.CommitAsync();
            return transaction;
        }
    }
}
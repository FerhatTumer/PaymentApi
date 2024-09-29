using Application.Commands;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Handlers
{
    public class CancelTransactionCommandHandler : IRequestHandler<CancelTransactionCommand, Transaction>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankService _bankService;
        public CancelTransactionCommandHandler(IUnitOfWork unitOfWork, IBankService bankService)
        {
            _unitOfWork = unitOfWork;
            _bankService = bankService;
        }
        public async Task<Transaction> Handle(CancelTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
                throw new Exception("Transaction not found.");
            await _bankService.Cancel(transaction);
            await _unitOfWork.CommitAsync();
            return transaction;
        }
    }
}
using Application.Commands;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankService _bankService;
        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IBankService bankService)
        {
            _unitOfWork = unitOfWork;
            _bankService = bankService;
        }
        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction(request.BankId, request.TotalAmount, request.OrderReference);
            await _unitOfWork.TransactionRepository.AddAsync(transaction);
            await _bankService.Pay(transaction);
            await _unitOfWork.CommitAsync();
            return transaction;
        }
    }
}
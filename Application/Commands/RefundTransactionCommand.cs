using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public record RefundTransactionCommand(Guid TransactionId, decimal Amount) : IRequest<Transaction>;
}
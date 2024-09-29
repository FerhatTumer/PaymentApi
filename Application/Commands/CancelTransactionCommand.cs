using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public record CancelTransactionCommand(Guid TransactionId) : IRequest<Transaction>;
}
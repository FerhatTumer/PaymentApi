using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public record CreateTransactionCommand(string BankId, decimal TotalAmount, string OrderReference) : IRequest<Transaction>;
}
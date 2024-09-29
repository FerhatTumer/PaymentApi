using Core.Entities;
using MediatR;
using System;
namespace Application.Queries
{
    public record GetTransactionByIdQuery(Guid TransactionId) : IRequest<Transaction>;
}
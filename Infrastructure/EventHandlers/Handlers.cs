using Core.Events;
using System;
using System.Threading.Tasks;
namespace Infrastructure.EventHandlers
{
    public class TransactionCancelledEventHandler : IEventHandler<TransactionCancelledEvent>
    {
        public Task Handle(TransactionCancelledEvent domainEvent)
        {
            // Logic for handling the transaction cancellation, e.g., logging, notifications, etc.
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was cancelled.");
            return Task.CompletedTask;
        }
    }
    public class TransactionSucceededEventHandler : IEventHandler<TransactionSucceededEvent>
    {
        public Task Handle(TransactionSucceededEvent domainEvent)
        {
            // Logic for handling the transaction success
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was successful.");
            return Task.CompletedTask;
        }
    }
    public class TransactionRefundedEventHandler : IEventHandler<TransactionRefundedEvent>
    {
        public Task Handle(TransactionRefundedEvent domainEvent)
        {
            // Logic for handling the transaction refund
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was refunded with amount: {domainEvent.RefundedAmount}.");
            return Task.CompletedTask;
        }
    }
}
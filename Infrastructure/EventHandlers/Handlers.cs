using Core.Entities;
using Core.Enums;
using Core.Events;
using Core.Interfaces;
using System;
using System.Threading.Tasks;
namespace Infrastructure.EventHandlers
{
    //We can add transaction detail here also....
    public class TransactionCancelledEventHandler : IEventHandler<TransactionCancelledEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionCancelledEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Handle(TransactionCancelledEvent domainEvent)
        {
            // Logic for handling the transaction cancellation, e.g., logging, notifications, etc.
            var detail = new TransactionDetail(domainEvent.TransactionId, TransactionType.Cancel.ToString(), domainEvent.Amount);
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was cancelled.");
            _unitOfWork.TransactionDetailRepository.AddAsync(detail);
            detail.MarkAsSuccess();
            return Task.CompletedTask;
        }
    }
    public class TransactionSucceededEventHandler : IEventHandler<TransactionSucceededEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionSucceededEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Handle(TransactionSucceededEvent domainEvent)
        {
            // Logic for handling the transaction success
            var detail = new TransactionDetail(domainEvent.TransactionId, TransactionType.Sale.ToString(), domainEvent.Amount);
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was successful.");
            _unitOfWork.TransactionDetailRepository.AddAsync(detail);
            detail.MarkAsSuccess();
            return Task.CompletedTask;
        }
    }
    public class TransactionRefundedEventHandler : IEventHandler<TransactionRefundedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRefundedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Handle(TransactionRefundedEvent domainEvent)
        {
            // Logic for handling the transaction refund
            Console.WriteLine($"Transaction {domainEvent.TransactionId} was refunded with amount: {domainEvent.RefundedAmount}.");
            var detail = new TransactionDetail(domainEvent.TransactionId, TransactionType.Refund.ToString(), domainEvent.RefundedAmount);
            _unitOfWork.TransactionDetailRepository.AddAsync(detail);
            detail.MarkAsSuccess();

            return Task.CompletedTask;
        }
    }
}
using Core.Entities;
using Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using Core.Interfaces;
namespace Application.Services
{
    public class BankService : IBankService
    {
        private readonly Dictionary<string, BaseBank> _banks;
        private readonly DomainEventDispatcher _eventDispatcher;
        public BankService(DomainEventDispatcher eventDispatcher)
        {
            _banks = new Dictionary<string, BaseBank>
           {
               { "bank1-id", new Akbank() },
               { "bank2-id", new Garanti() },
               { "bank3-id", new YapiKredi() }
           };
            _eventDispatcher = eventDispatcher;
        }
        public async Task Pay(Transaction transaction)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                await _banks[transaction.BankId].Pay(transaction);
                await _eventDispatcher.Dispatch(transaction.DomainEvents);
                transaction.ClearDomainEvents();
            }
            else
            {
                throw new InvalidOperationException("Bank not found.");
            }
        }
        public async Task Cancel(Transaction transaction)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                await _banks[transaction.BankId].Cancel(transaction);
                await _eventDispatcher.Dispatch(transaction.DomainEvents);
                transaction.ClearDomainEvents();
            }
            else
            {
                throw new InvalidOperationException("Bank not found.");
            }
        }
        public async Task Refund(Transaction transaction, decimal amount)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                await _banks[transaction.BankId].Refund(transaction, amount);
                await _eventDispatcher.Dispatch(transaction.DomainEvents);
                transaction.ClearDomainEvents();
            }
            else
            {
                throw new InvalidOperationException("Bank not found.");
            }
        }
    }
}
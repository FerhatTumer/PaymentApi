using Core.Entities;
using Core.Interfaces;
using System;

namespace Application.Services
{
    public class BankService : IBankService
    {
        private readonly Dictionary<string, BaseBank> _banks;
        public BankService()
        {
            _banks = new Dictionary<string, BaseBank>
           {
               { new string("bank1-id"), new Akbank() },
               { new string("bank2-id"), new Garanti() },
               { new string("bank3-id"), new YapiKredi() }
           };
        }
        public Task Pay(Transaction transaction)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                return _banks[transaction.BankId].Pay(transaction);
            }
            throw new InvalidOperationException("Bank not found.");
        }
        public Task Cancel(Transaction transaction)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                return _banks[transaction.BankId].Cancel(transaction);
            }
            throw new InvalidOperationException("Bank not found.");
        }
        public Task Refund(Transaction transaction, decimal amount)
        {
            if (_banks.ContainsKey(transaction.BankId))
            {
                return _banks[transaction.BankId].Refund(transaction, amount);
            }
            throw new InvalidOperationException("Bank not found.");
        }
    }
}
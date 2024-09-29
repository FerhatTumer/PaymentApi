using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class BaseBank
    {
        public abstract Task Pay(Transaction transaction);
        public abstract Task Cancel(Transaction transaction);
        public abstract Task Refund(Transaction transaction, decimal amount);
    }
}

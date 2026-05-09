using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCrown.TaxInspectionConsole2
{
    public class TransactionCreatedEvent
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}

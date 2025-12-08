using System;
using System.Collections.Generic;
using System.Text;

namespace CoinChronicle
{
    public class ChronicleEntry
    {
        public Guid Id { get; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }




        public ChronicleEntry(DateTime date, decimal amount, bool isCredit)
        {
            Id = Guid.NewGuid();
            Date = date;
            Amount = amount;
            IsCredit = isCredit;

        }
    }
}

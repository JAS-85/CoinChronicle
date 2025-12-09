using System;
using System.Collections.Generic;
using System.Text;

namespace CoinChronicle
{
    // todo: add save lastId to not have dublicates between instances.
    public class ChronicleEntry
    {
        private static int _nextId = 1;

        public string Id { get; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }


        public ChronicleEntry(DateTime date, string title, decimal amount, bool isCredit)
        {
            Id = (_nextId++).ToString();
            Date = date;
            Title = title;
            Amount = amount;
            IsCredit = isCredit;
        }
    }
}

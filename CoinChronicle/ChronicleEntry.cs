using System;

namespace CoinChronicle
{
    public class ChronicleEntry
    {
        private static int _nextId = 1;

        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        // For deserialization
        public ChronicleEntry()
        {
        }

        public ChronicleEntry(DateTime date, string title, decimal amount, bool isCredit)
        {
            Id = (_nextId++).ToString();
            Date = date;
            Title = title;
            Amount = amount;
            IsCredit = isCredit;
        }

        internal static void SetNextId(int next) => _nextId = next;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

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


        // Constructor taking id was necessary to reconstruct on loading
        public ChronicleEntry(string id, DateTime date, string title, decimal amount, bool isCredit)
        {
            Id = id;
            Date = date;
            Title = title;
            Amount = amount;
            IsCredit = isCredit;
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

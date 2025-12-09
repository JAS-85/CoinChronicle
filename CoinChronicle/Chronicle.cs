using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CoinChronicle
{

    // todo: formating of printing, add comment/title prop in print. Colour entry according to IsCredit (or debit).
    // format print of date to exclude t
    public class Chronicle
    {
        private readonly List<ChronicleEntry> _chronicle;

        public Chronicle()
        {
            _chronicle = new List<ChronicleEntry>();
        }

        public IReadOnlyList<ChronicleEntry> Entries => _chronicle.AsReadOnly();

        public void PrintAll(string amountFilter, string sortType, string sortOrder)
        {
            IEnumerable<ChronicleEntry> query = _chronicle;
            if (amountFilter == "c") query = query.Where(e => e.IsCredit);
            else if (amountFilter == "d") query = query.Where(e => !e.IsCredit);
            // "a" or anything else => no filter (All)

            bool desc = sortOrder == "desc" || sortOrder == "d";
            if (sortType == "m")
                query = desc ? query.OrderByDescending(e => e.Date) : query.OrderBy(e => e.Date);

            else if (sortType == "a")
                query = desc ? query.OrderByDescending(e => e.Amount) : query.OrderBy(e => e.Amount);

            else if (sortType == "t")
                query = desc ? query.OrderByDescending(e => e.Title) : query.OrderBy(e => e.Title);

            else
                query = desc ? query.OrderByDescending(e => e.Date) : query.OrderBy(e => e.Date);

            foreach (var e in query)
                Console.WriteLine($"{e.Id}, {e.Date:yyyy-MM-dd}, {e.Amount}, {e.IsCredit}");
        }

        public void PrintDebit(string amountFilter, string sortType, string sortOrder)
        {
            foreach (var e in _chronicle.Where(e => !e.IsCredit).OrderBy(e => e.Date))
            {
                Console.WriteLine($"{e.Id}, {e.Date}, {e.Amount}, {e.IsCredit}");
            }
        }

        public void PrintCredit(string amountFilter, string sortType, string sortOrder)
        {
            foreach (var e in _chronicle.Where(e => e.IsCredit).OrderBy(e => e.Date))
            {
                Console.WriteLine($"{e.Id}, {e.Date}, {e.Amount}, {e.IsCredit}");
            }
        }

        public void ReturnEntry(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid id");
                return;
            }

            var match = _chronicle
                .Where(e => e.Id == id)
                .OrderBy(e => e.Date);

            var entry = _chronicle.FirstOrDefault(e => e.Id == id);
            if (entry == null) Console.WriteLine("Not found");
            else
            {
                Console.WriteLine($"{entry.Id}, {entry.Date}, {entry.Amount}, {entry.IsCredit}");
                Modify(entry);
            }
        }

        public void Modify(ChronicleEntry entry)
        {
            // todo: reuse procedure to add new ChronicleEntry to overwrite/modify existing.
        }

        public void Add(ChronicleEntry entry)
        {
            {
                _chronicle.Add(entry);
            }
        }

    }
}

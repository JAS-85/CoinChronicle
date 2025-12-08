using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace CoinChronicle
{
    public class Chronicle
    {
        private readonly List<ChronicleEntry> _chronicle;

        public Chronicle()
        {
            _chronicle = new List<ChronicleEntry>();
        }

        public IReadOnlyList<ChronicleEntry> Entries => _chronicle.AsReadOnly();

        public void PrintAll()
        {
            foreach (var e in _chronicle.OrderBy(e => e.Date))
            {
                Console.WriteLine(e);
            }
        }

        public void PrintDebit()
        {
            foreach (var e in _chronicle.Where(e=> !e.IsCredit).OrderBy(e => e.Date))
            {
                Console.WriteLine(e);
            }
        }

        public void PrintCredit()
        {
            foreach (var e in _chronicle.Where(e => e.IsCredit).OrderBy(e=>e.Date))
            {
                Console.WriteLine(e);
            }
        }
    }
}

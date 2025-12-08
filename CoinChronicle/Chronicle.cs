using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace CoinChronicle
    // This is the Chronicle containing, managing - adding, removing, editing chronicle entries.
{
    public class Chronicle
    {
        private readonly List<ChronicleEntry> _chronicle;

        public Chronicle()
        {
            _chronicle = new List<ChronicleEntry>();
        }

    }
}

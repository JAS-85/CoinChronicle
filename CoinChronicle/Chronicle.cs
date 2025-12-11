using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public void Print(string amountFilter, string sortType, string sortOrder)
        {
            Console.Write("\n\n");
            IEnumerable<ChronicleEntry> query = _chronicle;
            if (amountFilter == "c") query = query.Where(e => e.IsCredit);
            else if (amountFilter == "d") query = query.Where(e => !e.IsCredit);

            bool desc = sortOrder == "desc" || sortOrder == "d";
            if (sortType == "m")
                query = desc ? query.OrderByDescending(e => e.Date) : query.OrderBy(e => e.Date);
            else if (sortType == "a")
                query = desc ? query.OrderByDescending(e => e.Amount) : query.OrderBy(e => e.Amount);
            else if (sortType == "t")
                query = desc ? query.OrderByDescending(e => e.Title) : query.OrderBy(e => e.Title);
            else
                query = desc ? query.OrderByDescending(e => e.Date) : query.OrderBy(e => e.Date);

            Console.WriteLine($"{"ID",-3} {"Date",-10} {"Amount",10} {"Type",-6} {"Title"}");
            foreach (var e in query)
            {
                Console.Write($"{e.Id,-3} {e.Date:yyyy-MM-dd} ");
                if (e.IsCredit)
                {
                    PrintHelper.Green($"{e.Amount,10:F2}");
                }
                else
                {
                    PrintHelper.Red($"{e.Amount,10:F2}");
                }
                Console.WriteLine($" {(e.IsCredit ? "Credit" : "Debit"),-6} {e.Title}");
            }
            Console.Write("\n\n");

        }

        public void FindEntry(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                PrintHelper.CyanArrowsMessage("Invalid id");
                return;
            }

            var entry = _chronicle.FirstOrDefault(e => e.Id == id);
            if (entry == null) PrintHelper.CyanArrowsMessage("Not found");

            else
            {
                Console.WriteLine($"{entry.Id}, {entry.Date:yyyy-MM-dd}, {entry.Amount}, {(entry.IsCredit ?
                    "Credit" : "Debit")}");
                PrintHelper.CyanArrowsMessage("(R)emove or (E)dit.");
                PrintHelper.WritePrompt();
                var input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                while (input != "r" && input != "e")
                {
                    PrintHelper.CyanArrowsMessage("Invalid entry. 'R' to remove, 'E' to edit.");
                    PrintHelper.WritePrompt();
                    input = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                }
                if (input == "r") _chronicle.Remove(entry);
                if (input == "e") Modify(entry);
            }
        }

        public void Modify(ChronicleEntry entry)
        {
            var validate = new ValidatorHelper();
            PrintHelper.CyanArrowsMessage("(D)ebit or (C)redit");
            PrintHelper.WritePrompt();
            var input2 = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            while (input2 != "d" && input2 != "c")
            {
                PrintHelper.CyanArrowsMessage("Invalid input. Enter 'C' or 'D'.");
                PrintHelper.WritePrompt();
                input2 = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            }

            bool isCredit = input2 != "d";

            PrintHelper.CyanArrowsMessage("Transaction date: 'YYYY-MM-DD'");
            PrintHelper.WritePrompt();
            input2 = Console.ReadLine();
            while (validate.IsDate(input2) == false)
            {
                PrintHelper.CyanArrowsMessage("Invalid date format. Enter 'YYYY-MM-DD' use '-' not spaces.");
                PrintHelper.WritePrompt();
                input2 = Console.ReadLine();
            }

            DateTime.TryParseExact(input2, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

            PrintHelper.CyanArrowsMessage("Title for entry. No spaces or special characters.");
            PrintHelper.WritePrompt();
            var title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title) || !title.All(char.IsLetterOrDigit))
            {
                PrintHelper.CyanArrowsMessage("Invalid input. Use letters and numbers.");
                PrintHelper.WritePrompt();
                title = Console.ReadLine();
            }

            PrintHelper.CyanArrowsMessage("Enter amount:");
            PrintHelper.WritePrompt();
            input2 = Console.ReadLine();
            while (validate.IsDecimal(input2) == false)
            {
                PrintHelper.CyanArrowsMessage("Invalid input. Numbers and ',' if needed. No spaces, no symbols, no letters.");
                PrintHelper.WritePrompt();
                input2 = Console.ReadLine();
            }
            var amount = decimal.Parse(input2, CultureInfo.InvariantCulture);

            entry.Date = date;
            entry.Title = title;
            entry.Amount = amount;
            entry.IsCredit = isCredit;
        }

        public void Add(ChronicleEntry entry) => _chronicle.Add(entry);

        public void ReplaceAll(IEnumerable<ChronicleEntry> entries)
        {
            _chronicle.Clear();
            _chronicle.AddRange(entries);
            int maxId = _chronicle
                .Select(e => int.TryParse(e.Id, out var v) ? v : 0)
                .DefaultIfEmpty(0)
                .Max();
            ChronicleEntry.SetNextId(maxId + 1);
        }

        public decimal GetBalance()
        {
            return _chronicle.Sum(e => e.IsCredit ? e.Amount : -e.Amount);
        }
    }
}

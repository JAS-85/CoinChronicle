using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void Print(string amountFilter, string sortType, string sortOrder)
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


        public void FindEntry(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid id");
                return;
            }

            var match = _chronicle
                .Where(e => e.Id == id);
            var entry = _chronicle.FirstOrDefault(e => e.Id == id);
            if (entry == null) Console.WriteLine("Not found");
            else
            {
                Console.WriteLine($"{entry.Id}, {entry.Date}, {entry.Amount}, {entry.IsCredit}");
                Console.WriteLine("To remove entry press 'R', to Edit press 'E'.");
                var input = (Console.ReadLine() ?? "").ToLowerInvariant();
                while (input != "r" &&  input != "e")
                {
                    Console.WriteLine("Invalid entry. 'R' to remove, 'E' to edit.");
                    input = (Console.ReadLine() ?? "").ToLowerInvariant();
                }
                if (input == "r") { _chronicle.Remove(entry); }
                if (input == "e") { Modify(entry); }

            }
        }

        //todo: replace the loop below with program.cs function when created.
        public void Modify(ChronicleEntry entry)

        {
            ValidatorHelper validate = new ValidatorHelper();
            PrintHelper.CyanArrowsMessage("Enter 'd' for Debit - 'c' for Credit\n");
            PrintHelper.Cyan(">> ");
            var input2 = (Console.ReadLine() ?? "").ToLowerInvariant();


            while (input2 != "d" && input2 != "c")
            {
                Console.WriteLine("Invalid input. Enter 'C' or 'D'.");
                input2 = (Console.ReadLine() ?? "").ToLowerInvariant();
            }

            bool isCredit = true;
            if (input2 == "d")
            {
                isCredit = false;

            }

            Console.WriteLine("Enter transactiondate: 'YYYY-MM-DD");
            input2 = Console.ReadLine();
            while (validate.IsDate(input2) == false)
            {
                Console.WriteLine("Invalid date format. Enter 'YYYY-MM-DD' use  ' - ' not spaces.");
                input2 = Console.ReadLine();
            }

            DateTime date;
            DateTime.TryParseExact(input2, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            Console.WriteLine("Enter a title for the transaction entry. Letters and numbers - no special characters, no spaces.");
            var title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title) || !title.All(char.IsLetterOrDigit))
            {
                Console.WriteLine("Invalid input. Letters and numbers.");
                title = Console.ReadLine();
            }

            Console.WriteLine("Enter amount:");
            input2 = Console.ReadLine();
            while (validate.IsDecimal(input2) == false)
            {
                Console.WriteLine("Invalid input. Numbers and ' , ' if you need decimal values. No spaces, no symbols, no letters.");
                input2 = Console.ReadLine();
            }
            var amount = decimal.Parse(input2);

            entry.Date = date;
            entry.Title = title;
            entry.Amount = amount;
            entry.IsCredit = isCredit;
        }

        public void Add(ChronicleEntry entry)
        {
            {
                _chronicle.Add(entry);
            }
        }

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
        


    }
}

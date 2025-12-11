using CoinChronicle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var dataPath = "coinchronicle.json";

        Chronicle chronicle = new Chronicle();
        var loaded = Keeper.Load(dataPath);
        if (loaded.Count > 0) chronicle.ReplaceAll(loaded);

        string balancePlaceholder = chronicle.GetBalance().ToString();
        bool showIntro = true;
        bool counting = true;

        while (counting)
        {
            MainMenu(showIntro);
            showIntro = false;
        }
       
        void MainMenu(bool showIntro)
        {
            var validate = new ValidatorHelper();
            balancePlaceholder = chronicle.GetBalance().ToString();

            if (showIntro)
            {
                Console.WriteLine(dataPath);
                showIntro = false;
            }

            PrintHelper.IntroText(balancePlaceholder);
            PrintHelper.WriteChoice1();
            PrintHelper.WriteChoice2();
            PrintHelper.WriteChoice3();
            PrintHelper.WriteChoice4();

            PrintHelper.WritePrompt();
            var input = Console.ReadLine();
            while (validate.MainMenuCheck(input) == false)
            {
                PrintHelper.WritePrompt();
                Console.WriteLine("Invalid input, use 1, 2, 3 or 4");
                PrintHelper.WritePrompt();
                input = Console.ReadLine();
            }

            if (input == "1")
            {
                PrintHelper.CyanArrowsMessage("'A' for All, 'D' for debit, 'C' for Credit.");
                PrintHelper.WritePrompt();
                var amountFilter = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                while (amountFilter != "a" && amountFilter != "d" && amountFilter != "c")
                {
                    PrintHelper.WritePrompt();
                    Console.WriteLine("Invalid entry. Enter 'A', 'D' or 'C'.");
                    PrintHelper.WritePrompt();
                    amountFilter = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                }

                PrintHelper.WritePrompt();
                Console.WriteLine("Sort by: 'M' for month, 'A' for amount, 'T' for title.");
                PrintHelper.WritePrompt();
                var sortType = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                while (sortType != "m" && sortType != "a" && sortType != "t")
                {
                    PrintHelper.WritePrompt();
                    Console.WriteLine("Wrong input. Use 'M', 'A' or 'T'.");
                    PrintHelper.WritePrompt();
                    sortType = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                }

                PrintHelper.WritePrompt();
                Console.WriteLine("'A' for ascending order, 'D' for descending order.");
                PrintHelper.WritePrompt();
                var sortOrder = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                while (sortOrder != "a" && sortOrder != "d")
                {
                    PrintHelper.WritePrompt();
                    Console.WriteLine("Wrond input, use 'A' or 'D'.");
                    PrintHelper.WritePrompt();
                    sortOrder = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                }

                chronicle.Print(amountFilter, sortType, sortOrder);
            }

            if (input == "2")
            {
                MenuChoice2();
            }

            if (input == "3")
            {
                PrintHelper.CyanArrowsMessage("Enter ID of entry to edit.");
                PrintHelper.WritePrompt();
                var input2 = Console.ReadLine() ?? "";
                chronicle.FindEntry(input2);
            }

            if (input == "4")
            {
                Keeper.Save(dataPath, chronicle.Entries);
                PrintHelper.WritePrompt();
                Console.WriteLine("All coins are accounted for.");
                counting = false;
            }
        }

        void MenuChoice2()
        {
            ValidatorHelper validate = new ValidatorHelper();

            PrintHelper.CyanArrowsMessage("(D)ebit or (C)redit ?");
            PrintHelper.WritePrompt();
            var input2 = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            while (input2 != "d" && input2 != "c")
            {
                PrintHelper.WritePrompt();
                Console.WriteLine("Invalid input. Enter 'C' or 'D'.");
                PrintHelper.WritePrompt();
                input2 = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            }

            bool isCredit = input2 != "d";

            PrintHelper.CyanArrowsMessage("Transaction date? 'YYYY-MM-DD'");
            PrintHelper.WritePrompt();
            input2 = Console.ReadLine();
            while (validate.IsDate(input2) == false)
            {
                PrintHelper.WritePrompt();
                Console.WriteLine("Invalid date format. Enter 'YYYY-MM-DD' use '-' not spaces.");
                PrintHelper.WritePrompt();
                input2 = Console.ReadLine();
            }

            DateTime.TryParseExact(input2, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

            PrintHelper.CyanArrowsMessage("Title for the transaction? - No spaces or special characters.");
            PrintHelper.WritePrompt();
            var title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title) || !title.All(char.IsLetterOrDigit))
            {
                PrintHelper.WritePrompt();
                Console.WriteLine("Invalid input. Use letters and/or numbers.");
                PrintHelper.WritePrompt();
                title = Console.ReadLine();
            }

            PrintHelper.CyanArrowsMessage("Enter amount:");
            PrintHelper.WritePrompt();
            input2 = Console.ReadLine();
            while (validate.IsDecimal(input2) == false)
            {
                PrintHelper.WritePrompt();
                Console.WriteLine("Invalid input. Numbers and ',' if needed. No spaces, no symbols, no letters.");
                PrintHelper.WritePrompt();
                input2 = Console.ReadLine();
            }
            var amount = decimal.Parse(input2, CultureInfo.InvariantCulture);

            ChronicleEntry entry = new ChronicleEntry(date, title, amount, isCredit);
            chronicle.Add(entry);

            PrintHelper.CyanArrowsMessage("To continue press any key. To create additional entry press 'C'");
            PrintHelper.WritePrompt();
            input2 = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            if (input2 == "c") { MenuChoice2(); }
        }

        var balance = decimal.Parse(balancePlaceholder, NumberStyles.Number, CultureInfo.InvariantCulture);
        var end = DateTime.UtcNow.AddSeconds(2);
        while (DateTime.UtcNow < end)
        {
            if ( balance < 0)
            {
                PrintHelper.Red("*");
            }
            else
            {
                PrintHelper.Green("*");
            }

            Thread.Sleep(60);
        }
        Console.WriteLine();
    }
}

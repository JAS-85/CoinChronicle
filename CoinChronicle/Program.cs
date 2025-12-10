// See https://aka.ms/new-console-template for more information
using CoinChronicle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var dataPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "coinchronicle.json");

        Chronicle chronicle = new Chronicle();
        var loaded = Keeper.Load(dataPath);
        if (loaded.Count > 0) chronicle.ReplaceAll(loaded);



        string balancePlaceholder = "1007";
        bool showIntro = true;
        bool counting = true;

        while (counting)
        {
            MainMenu(showIntro);

        }

        void MainMenu(bool showIntro)
        {
            ValidatorHelper validate = new ValidatorHelper();

            if (!showIntro)
            {
                PrintHelper.WriteChoice1();
                PrintHelper.WriteChoice2();
                PrintHelper.WriteChoice3();
                PrintHelper.WriteChoice4();
            }
            if (showIntro)
            {
                PrintHelper.IntroText(balancePlaceholder);
            }


            var input = Console.ReadLine();
            while (validate.MainMenuCheck(input) == false)
            {
                Console.WriteLine("Invalid input, use 1, 2, 3 or 4");
                input = Console.ReadLine();
            }

            if (input == "1")
            {
                PrintHelper.CyanArrowsMessage("'A' for All, 'D' for debit, 'C' for Credit.");
                PrintHelper.Cyan(">> ");

                var amountFilter = (Console.ReadLine() ?? "").ToLowerInvariant();
                while (amountFilter != "a" && amountFilter != "d" && amountFilter != "c")
                {
                    Console.WriteLine("Invalid entry. Enter 'A', 'D' or 'C'.");
                    amountFilter = (Console.ReadLine() ?? "").ToLowerInvariant(); ;
                }

                Console.WriteLine("Sort by: 'M' for month, 'A' for amount, 'T' for title.");
                var sortType = (Console.ReadLine() ?? "").ToLowerInvariant();
                while (sortType != "m" && sortType != "a" && sortType != "t")
                {
                    Console.WriteLine("Wrong input. Use 'M', 'A' or 'T'.");
                    sortType = (Console.ReadLine() ?? "").ToLowerInvariant();
                }

                Console.WriteLine("'A' for ascending order, 'D' for descending order.");
                var sortOrder = (Console.ReadLine() ?? "").ToLowerInvariant();
                while (sortOrder != "a" && sortOrder != "d")
                {
                    Console.WriteLine("Wrond input, use 'A' or 'D'.");
                    sortOrder = (Console.ReadLine() ?? "").ToLowerInvariant();
                }

                chronicle.Print(amountFilter, sortType, sortOrder);
                
            }

            if (input == "2")
            {
                MenuChoice2();
            }

            if (input == "3")
            {
                PrintHelper.CyanArrowsMessage("Enter ID of entry to edit.\n");
                PrintHelper.Cyan(">> ");
                var input2 = Console.ReadLine() ?? "";
                chronicle.FindEntry(input2);


            }

            if (input == "4")
            {
                Keeper.Save(dataPath, chronicle.Entries);
                Console.WriteLine("All coins are accounted for.");
                counting = false;
            }

        }

        void MenuChoice2()
        {
            ValidatorHelper validate = new ValidatorHelper();
            {
                PrintHelper.CyanArrowsMessage("Enter 'd' for Debit - 'c' for Credit\n");
                PrintHelper.Cyan(">> ");
                var input2 = (Console.ReadLine() ?? "").ToLowerInvariant(); ;


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

                // todo: move to top in choice 2 and add each prop as it goes for readability.
                ChronicleEntry entry = new ChronicleEntry(date, title, amount, isCredit);
                chronicle.Add(entry);

                Console.WriteLine("To continue press any key. To create additional entry press 'C'");
                input2 = (Console.ReadLine() ?? "").ToLowerInvariant();
                if (input2 == "c") { MenuChoice2(); }
            }
        }
    }
}
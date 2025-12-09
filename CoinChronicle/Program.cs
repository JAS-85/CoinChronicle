// See https://aka.ms/new-console-template for more information
using CoinChronicle;
using System;
using System.Globalization;

Chronicle chronicle = new Chronicle();
string balancePlaceholder = "1007";
bool showIntro = true;

while (true)
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
        showIntro = false;
    }


    var input = Console.ReadLine();
    while (validate.MainMenuCheck(input)==false)
    {
        Console.WriteLine("Invalid input, use 1, 2, 3 or 4");
        input= Console.ReadLine();
    }
    
    Console.Write(input);
    // todo: Create and add sorting functions restructure the while if's to work with all options.
    if (input == "1")
    {
        PrintHelper.CyanArrowsMessage("'A' for All, 'D' for debit, 'C' for Credit.");
        PrintHelper.Cyan(">> ");

        var amountFilter = (Console.ReadLine() ?? "").ToLowerInvariant();
        while (amountFilter != "a" && amountFilter != "d" && amountFilter != "c")
        {
                Console.WriteLine("Invalid entry. Enter 'A', 'D' or 'C'.");
                amountFilter= (Console.ReadLine() ?? "").ToLowerInvariant(); ;
        }

        Console.WriteLine("Sort by: 'M' for month, 'A' for amount, 'T' for title.");
        var sortType = (Console.ReadLine() ?? "").ToLowerInvariant();
        while (sortType != "m" && sortType != "a" && sortType != "t")
        {
            Console.WriteLine("Wrong input. Use 'M', 'A' or 'T'.");
            sortType= (Console.ReadLine() ?? "").ToLowerInvariant();
        }

        Console.WriteLine("'A' for ascending order, 'D' for descending order.");
        var sortOrder = (Console.ReadLine() ?? "").ToLowerInvariant();
        while (sortOrder != "a" && sortOrder != "d")
        {
            Console.WriteLine("Wrond input, use 'A' or 'D'.");
            sortOrder = (Console.ReadLine() ?? "").ToLowerInvariant();
        }

       
        if (amountFilter == "a")
        {
            chronicle.PrintAll(amountFilter, sortType, sortOrder); // add value input to print functions to enable input decide output sorted by.

        }

        if (amountFilter == "d")
        {
            chronicle.PrintDebit(amountFilter, sortType, sortOrder);

        }

        if (amountFilter == "c")
        {
            chronicle.PrintCredit(amountFilter, sortType, sortOrder);

        }
    }

    if (input == "2")
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
        while (validate.IsDate(input2)==false)
        {
            Console.WriteLine("Invalid date format. Enter 'YYYY-MM-DD' with '-' no spaces.");
            input2 = Console.ReadLine();
        }

        DateTime date;
        DateTime.TryParseExact(input2, "yyyy-MM-dd",
        CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        Console.WriteLine("Enter a title for the transaction entry. Use letters and/or numbers, no special characters or spaces.");
        var title = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(title) || !title.All(char.IsLetterOrDigit))
        {
            Console.WriteLine("Invalid input. Use letters and/or numbers.");
            title = Console.ReadLine();
        }

        Console.WriteLine("Enter amount:");
        input2 = Console.ReadLine();
        while (validate.IsDecimal(input2)==false)
        {
            Console.WriteLine("Invalid number, use numbers and ' , ' if you need decimal values - no spaces, symbols or letters.");
            input2 = Console.ReadLine();
        }
        decimal amount;
        Decimal.TryParse(input2, out amount);

        ChronicleEntry entry = new ChronicleEntry(date, title, amount, isCredit);
        chronicle.Add(entry);
        // todo: add choice of additional entry or return to main menu.
    }

    if (input == "3")
    {//todo: create and point to function to fetch corresponding ChronicleEntry for edit and saving
        PrintHelper.CyanArrowsMessage("Enter ID of enrtry to edit.\n");
        PrintHelper.Cyan(">> ");
        var input2 = Console.ReadLine() ?? "" ;

        
        // todo: validate input send request to chronicle, return entry
    }

    if (input == "4")
    {
        // todo: call on keeper to save before exit
    }
    
    else { }//todo: return wrong input }
}



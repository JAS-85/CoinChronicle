// See https://aka.ms/new-console-template for more information
using CoinChronicle;

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
    while (!validate.MainMenuCheck(input))
    {
        Console.WriteLine("Invalid input, use 1, 2, 3 or 4");
        input= Console.ReadLine();
    }
    
    Console.Write(input);

    if (input == "1")
    {// todo: create and point to function that utilizes the existing Chronicle linq functions.
        PrintHelper.CyanArrowsMessage("'A' for All, 'D' for debit, 'C' for Credit.");
        PrintHelper.Cyan(">> ");
        var input2 = Console.ReadLine();
        if (input2.Equals("A", StringComparison.OrdinalIgnoreCase))
        {

        }
        // todo: validate input, return chronicle
    }

    if (input == "2")
    {// todo: create and point to funtion creating ChronicleEntry
        PrintHelper.CyanArrowsMessage("Enter 'd' for Debit - 'c' for Credit\n");
        PrintHelper.Cyan(">> ");
        var input2 = Console.ReadLine() ?? "";
        // todo: validate input and store to chronicle
    }

    if (input == "3")
    {//todo: create and point to function to fetch corresponding ChronicleEntry for edit and saving
        PrintHelper.CyanArrowsMessage("Enter ID of enrtry to edit.\n");
        PrintHelper.Cyan(">> ");
        var input2 = Console.ReadLine();
        // todo: validate input send request to chronicle, return entry
    }

    if (input == "4")
    {
        // todo: call on keeper to save before exit
    }

    else { }//todo: return wrong input }
}
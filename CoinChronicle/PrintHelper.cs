using System;

namespace CoinChronicle
{
    static public class PrintHelper
    {
        public static void IntroText(string balancePlaceholder)
        {
            WritePrompt();
            Console.Write("Welcome to CoinChronicle\n");

            WritePrompt();
            Console.Write($"Your current balance: {balancePlaceholder}\n");

            WritePrompt();
            Console.Write("Choose an option:\n");

        }
        
        public static void WriteChoice1()
        {
            WritePrompt();
            Console.Write("(");
            Magenta("1");
            Console.Write(") ");
            Console.Write("Show chronicle entries (All");
            Cyan("/");
            Console.Write("Expense(s)");
            Cyan("/");
            Console.Write("Income(s)\n");
        }

        public static void WriteChoice2()
        {
            WritePrompt();
            Console.Write("(");
            Magenta("2");
            Console.Write(") ");
            Console.Write("Add new Expense");
            Cyan("/");
            Console.Write("Income\n");
        }

        public static void WriteChoice3()
        {
            WritePrompt();
            Console.Write("(");
            Magenta("3");
            Console.Write(") ");
            Console.Write("Edit Item (edit, remove)\n");
        }

        public static void WriteChoice4()
        {
            WritePrompt();
            Console.Write("(");
            Magenta("4");
            Console.Write(") ");
            Console.Write("Save and Quit\n");
        }

        public static void Cyan(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.ForegroundColor = pre;
        }

        public static void Magenta(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.ForegroundColor = pre;
        }

        public static void WritePrompt()
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ForegroundColor = pre;
        }

        public static void CyanArrowsMessage(string text)
        {
            WritePrompt();
            Console.WriteLine(text);
        }

        public static void Red(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = pre;
        }

        public static void Green(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = pre;
        }

    }
}

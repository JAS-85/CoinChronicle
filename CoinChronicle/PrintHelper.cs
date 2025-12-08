using System;
using System.Collections.Generic;
using System.Text;

namespace CoinChronicle
{ //todo: look into less tedious way of creating suitable interface
    static public class PrintHelper
    {
        public static void IntroText(string balancePlaceholder)
        {
            PrintHelper.Cyan(">> ");
            Console.Write("Welcome to CoinChronicle\n");

            PrintHelper.Cyan(">> ");
            Console.Write($"You have currently {balancePlaceholder}x kr on you account.\n");


            PrintHelper.Cyan(">> ");
            Console.Write("Pick an option:\n");

            PrintHelper.WriteChoice1();
            PrintHelper.WriteChoice2();
            PrintHelper.WriteChoice3();
            PrintHelper.WriteChoice4();

        }


        public static void WriteChoice1()
        {
            PrintHelper.Cyan(">> ");
            Console.Write("(");
            PrintHelper.Magenta("1");
            Console.Write(") ");
            Console.Write("Show items (All");
            PrintHelper.Cyan("/");
            Console.Write("Expense(s)");
            PrintHelper.Cyan("/");
            Console.Write("Income(s)\n");

        }

        public static void WriteChoice2()
        {
            PrintHelper.Cyan(">> ");
            Console.Write("(");
            PrintHelper.Magenta("2");
            Console.Write(") ");
            Console.Write("Add new Expense");
            PrintHelper.Cyan("/");
            Console.Write("Income\n");

        }

        public static void WriteChoice3()
        {
            PrintHelper.Cyan(">> ");
            Console.Write("(");
            PrintHelper.Magenta("3");
            Console.Write(") ");
            Console.Write("Edit Item (edit, remove)\n");

        }

        public static void WriteChoice4()
        {
            PrintHelper.Cyan(">> ");
            Console.Write("(");
            PrintHelper.Magenta("4");
            Console.Write(") ");
            Console.Write("Save and Quit\n");
            PrintHelper.Cyan(">> ");

        }

        public static void Cyan(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{text}");
            Console.ForegroundColor = pre;
        }

        public static void Magenta(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.ForegroundColor = pre;
        }
        public static void CyanArrowsMessage(string text)
        {
            var pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ForegroundColor = pre;
            Console.Write($"{text}");

        }
    }
}

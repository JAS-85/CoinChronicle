using System;
using System.Collections.Generic;
using System.Text;

namespace CoinChronicle
{
    public class ValidatorHelper
    {
        public bool MainMenuCheck(string input)
        {
            var check = input;

            if (input.ToString().ToLower().Equals("1") || input.ToString().ToLower().Equals("2") ||
                input.ToString().ToLower().Equals("3") || input.ToString().ToLower().Equals("3"))
            {
                return true;
            }

            // todo: implement specific error handlings?
            if (input.IsWhiteSpace()) { return false; }
            if (input == null) { return false; }
            if (input == "") { return false; }

            else return false;
        }

        public bool Menu1Check(string input)
        {
            if (input.ToString().ToLower().Equals("a") || input.ToString().ToLower().Equals("c") ||
                input.ToString().ToLower().Equals("d"))
            { 
                return true;
            }
            else return false;
        }


    }
}

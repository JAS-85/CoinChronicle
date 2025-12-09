using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (string.IsNullOrEmpty(input)) { return false; }

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

        public bool IsDate(string input)
        {
            if (DateTime.TryParseExact(input, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(input)) { return false; }

            else return false;
        }
        public bool IsDecimal(string input)
        {
            if (Decimal.TryParse(input, out _))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(input)) { return false; }

            else return false;
        }

    }
}

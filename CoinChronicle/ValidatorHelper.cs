using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CoinChronicle
{
    public class ValidatorHelper
    {
        public bool MainMenuCheck(string input) =>
            input is not null && (input == "1" || input == "2" || input == "3" || input == "4");

        public bool Menu1Check(string input) =>
            input is not null && (input.ToLowerInvariant() == "a" || 
            input.ToLowerInvariant() == "c" || input.ToLowerInvariant() == "d");

        // todo: set limit of valid timeframe
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

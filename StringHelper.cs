using System.Linq;

namespace NodaTimeSpike
{
    public static class StringHelper
    {
        public static string LettersOnly(this string value)
        {
            return new string(value.Where(char.IsLetter).ToArray());
        }

        public static string AssureEndsWithPeriod(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            value = value.Trim();
            return value.EndsWith(".") ? value : $"{value}.";
        }
    }
}
using System.Diagnostics.Contracts;
using System.Linq;

namespace WebParsingFramework.Utils
{
    public static class StringExtensions
    {
        [Pure]
        public static string RemoveNonDigitChars(this string input)
        {
            char[] chars = input.Where(char.IsDigit).ToArray();
            return new string(chars);
        }

        [Pure]
        public static bool IsEmptyOrWhiteSpace(this string input)
        {
            return input != null && string.IsNullOrWhiteSpace(input);
        }
    }
}
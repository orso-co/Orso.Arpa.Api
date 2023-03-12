using System.Text.RegularExpressions;

namespace Orso.Arpa.Misc
{
    public static partial class StringExtensions
    {
        [GeneratedRegex("\\s+")]
        private static partial Regex WhitespaceRegex();

        private static readonly Regex sWhitespace = WhitespaceRegex();

        public static string RemoveAllWhitespaces(this string str)
        {
            return sWhitespace.Replace(str, string.Empty);
        }

        public static string FormatItalic(this string str)
        {
            return $"_{str}_";
        }

        public static string FormatLink(this string href, string label)
        {
            return $"<{href}|{label}>";
        }
    }
}

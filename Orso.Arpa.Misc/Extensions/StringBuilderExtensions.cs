using System.Text;

namespace Orso.Arpa.Misc.Extensions
{
    public static class StringBuilderExtensions
    {
        private const string UNIX_LINE_BREAK = "\n";

        public static StringBuilder AppendBold(this StringBuilder builder, string text)
        {
            return builder.Append('*').Append(text).Append('*');
        }

        public static StringBuilder AppendItalic(this StringBuilder builder, string text)
        {
            return builder.Append('_').Append(text).Append('_');
        }

        public static StringBuilder AppendQuote(this StringBuilder builder, string text)
        {
            return builder.Append('>').Append(text);
        }

        public static StringBuilder AppendUnixLine(this StringBuilder builder)
        {
            return builder.Append(UNIX_LINE_BREAK);
        }
    }
}

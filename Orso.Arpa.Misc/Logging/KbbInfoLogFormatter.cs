using System.Collections.Generic;
using System.Text;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Misc.Logging
{
    public static class KbbInfoLogFormatter
    {
        public static string FormatLog(
            string header,
            IDictionary<string, object> infoLines,
            string subHeader = null)
        {
            var messageStringBuilder = new StringBuilder();
            _ = messageStringBuilder.AppendBold(header.ToUpper()).AppendUnixLine();
            if (subHeader != null)
            {
                _ = messageStringBuilder.AppendItalic(subHeader).AppendUnixLine();
            }
            _ = messageStringBuilder.AppendUnixLine();
            foreach (string key in infoLines.Keys)
            {
                _ = messageStringBuilder.AppendQuote(key).Append(": {").Append(key.RemoveAllWhitespaces()).Append('}').AppendUnixLine();
            }
            _ = messageStringBuilder.AppendUnixLine();
            _ = messageStringBuilder.Append("#KBB#"); // this is the marker for the nlog filter

            return messageStringBuilder.ToString();
        }
    }
}

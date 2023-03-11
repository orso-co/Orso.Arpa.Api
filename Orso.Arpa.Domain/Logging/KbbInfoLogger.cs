using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Orso.Arpa.Domain.Logging
{
    public static partial class KbbInfoLogger
    {
        [GeneratedRegex("\\s+")]
        private static partial Regex WhitespaceRegex();

        private static readonly Regex sWhitespace = WhitespaceRegex();
        public static string RemoveAllWhitespace(string input)
        {
            return sWhitespace.Replace(input, string.Empty);
        }

        public static void LogInfoForKbb(
            ILogger logger,
            string header,
            IDictionary<string, object> infoLines,
            string subHeader = null)
        {
            var messageStringBuilder = new StringBuilder();
            _ = messageStringBuilder.Append('*').Append(header.ToUpper()).Append('*').AppendLine();
            if (subHeader != null)
            {
                _ = messageStringBuilder.Append("_(").Append(subHeader).AppendLine(")_");
            }
            _ = messageStringBuilder.AppendLine();
            foreach (KeyValuePair<string, object> line in infoLines)
            {
                _ = messageStringBuilder.Append('>').Append(line.Key).Append(": {").Append(RemoveAllWhitespace(line.Key)).Append('}').AppendLine();
            }
            _ = messageStringBuilder.AppendLine();
            _ = messageStringBuilder.Append("#KBB#"); // this is the marker for the nlog filter

            logger.LogInformation(messageStringBuilder.ToString(), infoLines.Values.Select(v => v ?? "_-- NONE --_").ToArray());
        }
    }
}

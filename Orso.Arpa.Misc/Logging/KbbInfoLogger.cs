using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Orso.Arpa.Misc.Logging
{
    public static class KbbInfoLogger
    {
        public static void LogInfoForKbb(
            ILogger logger,
            string header,
            IDictionary<string, object> infoLines,
            string subHeader = null)
        {
            logger.LogInformation(
#pragma warning disable CA2254 // Template should be a static expression
                KbbInfoLogFormatter.FormatLog(header, infoLines, subHeader),
#pragma warning restore CA2254 // Template should be a static expression
                infoLines.Values.Select(v => v ?? "-- NONE --".FormatItalic()).ToArray());
        }
    }
}

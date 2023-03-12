using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Orso.Arpa.Misc.Logging
{
    public static partial class KbbInfoLogger
    {
        public static void LogInfoForKbb(
            ILogger logger,
            string header,
            IDictionary<string, object> infoLines,
            string subHeader = null)
        {
            logger.LogInformation(
                KbbInfoLogFormatter.FormatLog(header, infoLines, subHeader),
                infoLines.Values.Select(v => v ?? "_-- NONE --_").ToArray());
        }
    }
}

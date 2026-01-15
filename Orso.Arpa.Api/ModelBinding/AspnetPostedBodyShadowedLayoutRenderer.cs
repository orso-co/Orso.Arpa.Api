using System.Collections.Generic;
using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using Orso.Arpa.Api.Middleware;

namespace Orso.Arpa.Api.ModelBinding
{
    [LayoutRenderer(SensibleRequestDataShadower.ASPNET_REQUEST_POSTED_BODY_SHADOWED)]
    public class AspnetPostedBodyShadowedLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            IDictionary<object, object> items = HttpContextAccessor.HttpContext?.Items;
            if (items == null || items.Count == 0)
            {
                return;
            }

            if (items.TryGetValue(SensibleRequestDataShadower.ASPNET_REQUEST_POSTED_BODY_SHADOWED, out var value))
            {
                _ = builder.Append(value as string);
            }
        }
    }
}

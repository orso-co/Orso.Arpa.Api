using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Orso.Arpa.Api.Middleware
{
    public static class SensibleRequestDataShadower
    {
        private const string SHADOW_VALUE = "**********";

        public const string ASPNET_REQUEST_POSTED_BODY_SHADOWED = "aspnet-request-posted-body-shadowed";

        public static async Task<string> ShadowSensibleDataForLoggingAsync(Stream body)
        {
            if (body?.CanSeek != true)
            {
                return string.Empty;
            }

            var originalPosition = body.Position;
            string bodyAsString;

            try
            {
                body.Position = 0;

                using var streamReader = new StreamReader(
                           body,
                           Encoding.UTF8,
                           true,
                           1024,
                           leaveOpen: true);
                bodyAsString = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
            finally
            {
                body.Position = originalPosition;
            }

            if (string.IsNullOrWhiteSpace(bodyAsString))
            {
                return string.Empty;
            }

            try
            {
                Dictionary<string, object> deserializedBody = JsonSerializer.Deserialize<Dictionary<string, object>>(bodyAsString);
                var shadowedBody = deserializedBody
                    .Select(item => item.Key.Contains("password", StringComparison.OrdinalIgnoreCase)
                        ? new KeyValuePair<string, object>(item.Key, SHADOW_VALUE)
                        : item)
                    .ToDictionary(x => x.Key, y => y.Value);
                var jso = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                };
                return JsonSerializer.Serialize(shadowedBody, jso);
            }
            catch (Exception)
            {
                return bodyAsString;
            }
        }
    }
}

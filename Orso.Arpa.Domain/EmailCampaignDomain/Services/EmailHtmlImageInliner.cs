using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Services;

/// <summary>
/// Extracts base64 data URIs from HTML, saves them as files, and replaces with public URLs.
/// This dramatically reduces HTML size (e.g. 8.5MB → 50KB) for faster SMTP delivery.
/// Uses string scanning instead of regex to avoid catastrophic memory usage on multi-MB base64 strings.
/// </summary>
public static class EmailHtmlImageInliner
{
    private const string DataUriMarker = "data:image/";

    private static readonly Dictionary<string, string> MimeToExtension = new(StringComparer.OrdinalIgnoreCase)
    {
        ["png"] = ".png",
        ["jpeg"] = ".jpeg",
        ["jpg"] = ".jpg",
        ["gif"] = ".gif",
        ["webp"] = ".webp",
        ["svg+xml"] = ".svg",
    };

    public static async Task<string> ReplaceBase64WithUrlsAsync(
        string html,
        string baseUrl,
        IEmailTemplateImageAccessor imageAccessor)
    {
        if (string.IsNullOrWhiteSpace(html) || imageAccessor == null)
        {
            return html;
        }

        // Quick check: does the HTML contain any data URIs?
        if (!html.Contains(DataUriMarker, StringComparison.OrdinalIgnoreCase))
        {
            return html;
        }

        var result = new StringBuilder(html.Length);
        int pos = 0;

        while (pos < html.Length)
        {
            // Find next data:image/ occurrence
            int dataUriStart = html.IndexOf(DataUriMarker, pos, StringComparison.OrdinalIgnoreCase);
            if (dataUriStart < 0)
            {
                result.Append(html, pos, html.Length - pos);
                break;
            }

            // Find the quote character that precedes this data URI (to know where src value started)
            int quotePos = dataUriStart - 1;
            while (quotePos >= pos && html[quotePos] != '"' && html[quotePos] != '\'')
            {
                quotePos--;
            }

            if (quotePos < pos || (html[quotePos] != '"' && html[quotePos] != '\''))
            {
                // No quote found, skip this occurrence
                result.Append(html, pos, dataUriStart + DataUriMarker.Length - pos);
                pos = dataUriStart + DataUriMarker.Length;
                continue;
            }

            char quoteChar = html[quotePos];

            // Extract image type (e.g., "png", "jpeg", "svg+xml")
            int typeStart = dataUriStart + DataUriMarker.Length;
            int semicolonPos = html.IndexOf(';', typeStart);
            if (semicolonPos < 0 || semicolonPos - typeStart > 20)
            {
                result.Append(html, pos, typeStart - pos);
                pos = typeStart;
                continue;
            }

            string imageType = html.Substring(typeStart, semicolonPos - typeStart);

            // Check for ";base64," marker
            const string base64Marker = ";base64,";
            if (!html.AsSpan(semicolonPos, Math.Min(base64Marker.Length, html.Length - semicolonPos))
                .Equals(base64Marker.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result.Append(html, pos, semicolonPos + 1 - pos);
                pos = semicolonPos + 1;
                continue;
            }

            int base64Start = semicolonPos + base64Marker.Length;

            // Find the closing quote — the end of the base64 data
            int closingQuote = html.IndexOf(quoteChar, base64Start);
            if (closingQuote < 0)
            {
                result.Append(html, pos, base64Start - pos);
                pos = base64Start;
                continue;
            }

            // Extract base64 data without creating regex match objects
            string base64Data = html.Substring(base64Start, closingQuote - base64Start)
                .Replace("\n", "").Replace("\r", "").Replace(" ", "");

            if (!MimeToExtension.TryGetValue(imageType, out string extension))
            {
                extension = ".png";
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                string contentType = $"image/{imageType}";
                string fileName = $"inline_{Guid.NewGuid():N}{extension}";

                string storedFileName = await imageAccessor.SaveAsync(fileName, contentType, imageBytes);
                string imageUrl = $"{baseUrl.TrimEnd('/')}/api/emailtemplates/images/{storedFileName}";

                // Append everything before the data URI, then the new URL
                result.Append(html, pos, quotePos + 1 - pos);
                result.Append(imageUrl);
                result.Append(quoteChar);
                pos = closingQuote + 1;
            }
            catch
            {
                // If extraction fails, keep the original base64
                result.Append(html, pos, closingQuote + 1 - pos);
                pos = closingQuote + 1;
            }
        }

        return result.ToString();
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Services;

/// <summary>
/// Extracts base64 data URIs from HTML, saves them as files, and replaces with public URLs.
/// This dramatically reduces HTML size (e.g. 8.5MB → 50KB) for faster SMTP delivery.
/// </summary>
public static class EmailHtmlImageInliner
{
    private static readonly Regex DataUriRegex = new(
        @"(src\s*=\s*[""'])data:image/(?<type>[a-zA-Z+]+);base64,(?<data>[A-Za-z0-9+/=\s]+)([""'])",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

        var matches = DataUriRegex.Matches(html);
        if (matches.Count == 0)
        {
            return html;
        }

        // Process matches in reverse order to preserve string positions
        var replacements = new List<(int Index, int Length, string Replacement)>();

        foreach (Match match in matches)
        {
            string imageType = match.Groups["type"].Value;
            string base64Data = match.Groups["data"].Value.Replace("\n", "").Replace("\r", "").Replace(" ", "");

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
                string imageUrl = $"{baseUrl.TrimEnd('/')}/api/emailtemplateimages/{storedFileName}";

                // Replace: src="data:image/..." → src="https://..."
                string replacement = $"{match.Groups[1].Value}{imageUrl}{match.Groups[4].Value}";
                replacements.Add((match.Index, match.Length, replacement));
            }
            catch
            {
                // If extraction fails, keep the original base64
            }
        }

        // Apply replacements in reverse order
        replacements.Sort((a, b) => b.Index.CompareTo(a.Index));
        foreach (var (index, length, replacement) in replacements)
        {
            html = string.Concat(html.AsSpan(0, index), replacement, html.AsSpan(index + length));
        }

        return html;
    }
}

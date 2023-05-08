using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;

namespace Orso.Arpa.Api.Middleware
{
    public class ArpaProfilePictureProvider : IImageProvider
    {
        private static readonly char[] SlashChars = { '\\', '/' };

        private Func<HttpContext, bool> match;

        private readonly FormatUtilities formatUtilities;

        public Func<HttpContext, bool> Match
        {
            get => match ?? IsMatch;
            set => match = value;
        }
        public ProcessingBehavior ProcessingBehavior { get; } = ProcessingBehavior.All;

        public ArpaProfilePictureProvider(
        FormatUtilities formatUtilities)
        {
            this.formatUtilities = formatUtilities;
        }

        public Task<IImageResolver> GetAsync(HttpContext context)
        {
            // ToDo: Get profile picture by persons service
            // https://github1s.com/SixLabors/ImageSharp.Web/blob/main/src/ImageSharp.Web.Providers.Azure/Providers/AzureBlobStorageImageProvider.cs
        }

        public bool IsValidRequest(HttpContext context)
        => formatUtilities.TryGetExtensionFromUri(context.Request.GetDisplayUrl(), out _);

        private bool IsMatch(HttpContext context)
        {
            // Only match loosly here for performance.
            // Path matching conflicts should be dealt with by configuration.
            string path = context.Request.Path.Value?.TrimStart(SlashChars);

            return path is not null
                && path.StartsWith("persons", StringComparison.OrdinalIgnoreCase)
                && path.EndsWith("profilepicture", StringComparison.OrdinalIgnoreCase);
        }
    }
}

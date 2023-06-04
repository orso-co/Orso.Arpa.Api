using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;
using SixLabors.ImageSharp.Web.Resolvers.Azure;

namespace Orso.Arpa.Api.Middleware
{
    public class ArpaProfilePictureProvider : IImageProvider
    {
        private static readonly char[] SlashChars = { '\\', '/' };

        private Func<HttpContext, bool> _match;

        public Func<HttpContext, bool> Match
        {
            get => _match ?? IsMatch;
            set => _match = value;
        }
        public ProcessingBehavior ProcessingBehavior { get; } = ProcessingBehavior.All;

        public async Task<IImageResolver> GetAsync(HttpContext context)
        {
            string path = context.Request.Path.Value?.TrimStart(SlashChars);
            string personIdAsString = path.Replace("api/persons/", "").Replace("/profilepicture", "");

            if (string.IsNullOrEmpty(personIdAsString) || !Guid.TryParse(personIdAsString, out Guid personId))
            {
                return null;
            }

            IArpaContext arpaContext = context.RequestServices.GetService<IArpaContext>();

            string profilePictureFileName = (await arpaContext.Persons.FindAsync(personId))?.ProfilePictureFileName;

            if (string.IsNullOrWhiteSpace(profilePictureFileName))
            {
                return null;
            }

            IFileAccessor fileAccessor = context.RequestServices.GetService<IFileAccessor>();

            BlobClient blob = await fileAccessor.GetAsBlobAsync(profilePictureFileName);

            return !await blob.ExistsAsync() ? null : (IImageResolver)new AzureBlobStorageImageResolver(blob);
        }

        public bool IsValidRequest(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return false;
            }

            string path = context.Request.Path.Value?.TrimStart(SlashChars);
            string personIdAsString = path.Replace("api/persons/", "").Replace("/profilepicture", "");

            if (string.IsNullOrEmpty(personIdAsString) || !Guid.TryParse(personIdAsString, out Guid personId))
            {
                return false;
            }

            IArpaContext arpaContext = context.RequestServices.GetService<IArpaContext>();
            return arpaContext.EntityExists<Person>(personId);
        }

        private bool IsMatch(HttpContext context)
        {
            if (!context.Request.Method.Equals(HttpMethods.Get))
            {
                return false;
            }

            // Only match loosly here for performance.
            // Path matching conflicts should be dealt with by configuration.
            string path = context.Request.Path.Value?.TrimStart(SlashChars);

            return path?.StartsWith("api/persons", StringComparison.OrdinalIgnoreCase) == true
                && path.EndsWith("profilepicture", StringComparison.OrdinalIgnoreCase);
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Infrastructure.FileManagement;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;

namespace Orso.Arpa.Api.Middleware
{
    /// <summary>
    /// ImageSharp provider for local file system storage.
    /// Used as an alternative to ArpaProfilePictureProvider when Azure Blob Storage is not available.
    /// </summary>
    public class LocalProfilePictureProvider : IImageProvider
    {
        private static readonly char[] SlashChars = ['\\', '/'];

        private Func<HttpContext, bool> _match;

        public Func<HttpContext, bool> Match
        {
            get => _match ?? IsMatch;
            set => _match = value;
        }

        public ProcessingBehavior ProcessingBehavior { get; } = ProcessingBehavior.All;

        public async Task<IImageResolver> GetAsync(HttpContext context)
        {
            string path = context.Request.Path.Value?.TrimStart(SlashChars) ?? "";
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

            var fileAccessor = context.RequestServices.GetService<IFileAccessor>();

            // For local storage, get the file path directly
            if (fileAccessor is LocalStorageProfilePictureAccessor localAccessor)
            {
                var filePath = localAccessor.GetFilePath(profilePictureFileName);
                if (!File.Exists(filePath))
                {
                    return null;
                }

                var fileInfo = new FileInfo(filePath);
                return new PhysicalFileSystemResolver(fileInfo);
            }

            // Fallback: try to get the file through IFileAccessor
            var fileResult = await fileAccessor.GetAsync(profilePictureFileName);
            if (fileResult?.Content == null)
            {
                return null;
            }

            return new InMemoryImageResolver(fileResult.Content, fileResult.LastModified);
        }

        public bool IsValidRequest(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return false;
            }

            string path = context.Request.Path.Value?.TrimStart(SlashChars) ?? "";
            string personIdAsString = path.Replace("api/persons/", "").Replace("/profilepicture", "");

            if (string.IsNullOrEmpty(personIdAsString) || !Guid.TryParse(personIdAsString, out Guid personId))
            {
                return false;
            }

            IArpaContext arpaContext = context.RequestServices.GetService<IArpaContext>();
            return arpaContext.EntityExists<Person>(personId);
        }

        private static bool IsMatch(HttpContext context)
        {
            if (!context.Request.Method.Equals(HttpMethods.Get))
            {
                return false;
            }

            string path = context.Request.Path.Value?.TrimStart(SlashChars);

            return path?.StartsWith("api/persons", StringComparison.OrdinalIgnoreCase) == true
                && path.EndsWith("profilepicture", StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// ImageSharp resolver for physical file system files.
    /// </summary>
    internal class PhysicalFileSystemResolver : IImageResolver
    {
        private readonly FileInfo _fileInfo;

        public PhysicalFileSystemResolver(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public Task<ImageMetadata> GetMetaDataAsync()
        {
            return Task.FromResult(new ImageMetadata(_fileInfo.LastWriteTimeUtc, _fileInfo.Length));
        }

        public Task<Stream> OpenReadAsync()
        {
            return Task.FromResult<Stream>(File.OpenRead(_fileInfo.FullName));
        }
    }

    /// <summary>
    /// ImageSharp resolver for in-memory byte arrays.
    /// </summary>
    internal class InMemoryImageResolver : IImageResolver
    {
        private readonly byte[] _content;
        private readonly DateTimeOffset _lastModified;

        public InMemoryImageResolver(byte[] content, DateTimeOffset lastModified)
        {
            _content = content;
            _lastModified = lastModified;
        }

        public Task<ImageMetadata> GetMetaDataAsync()
        {
            return Task.FromResult(new ImageMetadata(_lastModified.UtcDateTime, _content.Length));
        }

        public Task<Stream> OpenReadAsync()
        {
            return Task.FromResult<Stream>(new MemoryStream(_content));
        }
    }
}

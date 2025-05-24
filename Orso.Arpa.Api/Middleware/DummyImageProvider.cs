using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Web.Middleware;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;

namespace Orso.Arpa.Api.Middleware
{
    /// <summary>
    /// A dummy image provider that doesn't attempt to resolve any images.
    /// Used as a fallback when the PhysicalFileSystemProvider can't find wwwroot.
    /// </summary>
    public class DummyImageProvider : IImageProvider
    {
        private Func<HttpContext, bool> _match = _ => false;

        /// <inheritdoc/>
        public bool IsValidRequest(HttpContext context) => false;

        /// <inheritdoc/>
        public Task<IImageResolver> GetAsync(HttpContext context) => Task.FromResult<IImageResolver>(null);

        /// <inheritdoc/>
        public ProcessingBehavior ProcessingBehavior { get; } = ProcessingBehavior.CommandOnly;

        /// <inheritdoc/>
        public Func<HttpContext, bool> Match
        {
            get => _match;
            set => _match = value;
        }
    }
}
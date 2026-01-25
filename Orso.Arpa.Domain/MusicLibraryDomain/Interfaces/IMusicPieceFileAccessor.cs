using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.MusicLibraryDomain
{
    /// <summary>
    /// Interface for accessing music piece files in storage
    /// </summary>
    public interface IMusicPieceFileAccessor
    {
        /// <summary>
        /// Save a file to storage
        /// </summary>
        Task<IFileResult> SaveAsync(IFormFile file, string fileName);

        /// <summary>
        /// Get a file from storage
        /// </summary>
        Task<IFileResult> GetAsync(string fileName);

        /// <summary>
        /// Delete a file from storage
        /// </summary>
        Task DeleteAsync(string fileName);

        /// <summary>
        /// Get file as blob client for streaming
        /// </summary>
        Task<BlobClient> GetAsBlobAsync(string fileName);
    }
}

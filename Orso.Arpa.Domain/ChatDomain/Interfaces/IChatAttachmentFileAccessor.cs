using System;
using System.IO;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.ChatDomain.Interfaces
{
    /// <summary>
    /// Interface for managing chat attachment file storage.
    /// </summary>
    public interface IChatAttachmentFileAccessor
    {
        /// <summary>
        /// Saves a file and returns the storage path.
        /// </summary>
        /// <param name="roomId">Chat room ID for organizing files</param>
        /// <param name="fileName">Original filename</param>
        /// <param name="contentType">MIME content type</param>
        /// <param name="data">File data</param>
        /// <returns>Storage path for the saved file</returns>
        Task<string> SaveAsync(Guid roomId, string fileName, string contentType, byte[] data);

        /// <summary>
        /// Gets a file by its storage path.
        /// </summary>
        /// <param name="storagePath">The storage path returned from SaveAsync</param>
        /// <returns>File data and metadata, or null if not found</returns>
        Task<ChatAttachmentFileResult> GetAsync(string storagePath);

        /// <summary>
        /// Deletes a file by its storage path.
        /// </summary>
        Task DeleteAsync(string storagePath);

        /// <summary>
        /// Gets the download URL for an attachment.
        /// </summary>
        string GetDownloadUrl(string storagePath);
    }

    public class ChatAttachmentFileResult
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime LastModified { get; set; }
    }
}

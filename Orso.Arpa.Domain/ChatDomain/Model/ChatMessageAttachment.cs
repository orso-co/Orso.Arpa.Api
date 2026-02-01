using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatMessageAttachment : BaseEntity
    {
        public ChatMessageAttachment(
            Guid? id,
            Guid chatMessageId,
            string fileName,
            string storagePath,
            string contentType,
            long fileSize,
            string thumbnailPath = null) : base(id)
        {
            ChatMessageId = chatMessageId;
            FileName = fileName;
            StoragePath = storagePath;
            ContentType = contentType;
            FileSize = fileSize;
            ThumbnailPath = thumbnailPath;
        }

        [JsonConstructor]
        protected ChatMessageAttachment()
        {
        }

        /// <summary>
        /// Reference to the chat message
        /// </summary>
        public Guid ChatMessageId { get; private set; }
        public virtual ChatMessage ChatMessage { get; private set; }

        /// <summary>
        /// Original filename (for display and download)
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Path in storage (Azure Blob or local filesystem)
        /// </summary>
        public string StoragePath { get; private set; }

        /// <summary>
        /// MIME type of the file (e.g., application/pdf, audio/mpeg, video/mp4, image/jpeg, text/plain)
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// Optional: Thumbnail path for images and videos
        /// </summary>
        public string ThumbnailPath { get; private set; }

        /// <summary>
        /// Helper to determine if this is an image
        /// </summary>
        public bool IsImage => ContentType?.StartsWith("image/") ?? false;

        /// <summary>
        /// Helper to determine if this is a video
        /// </summary>
        public bool IsVideo => ContentType?.StartsWith("video/") ?? false;

        /// <summary>
        /// Helper to determine if this is audio
        /// </summary>
        public bool IsAudio => ContentType?.StartsWith("audio/") ?? false;

        /// <summary>
        /// Helper to determine if this is a PDF
        /// </summary>
        public bool IsPdf => ContentType == "application/pdf";
    }
}

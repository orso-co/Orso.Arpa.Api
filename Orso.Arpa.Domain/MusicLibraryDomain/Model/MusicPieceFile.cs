using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents a sheet music file (PDF, image, etc.)
    /// </summary>
    public class MusicPieceFile : BaseEntity
    {
        public MusicPieceFile(Guid? id, UploadMusicPieceFile.Command command, string storageFileName) : base(id)
        {
            MusicPieceId = command.MusicPieceId;
            MusicPiecePartId = command.MusicPiecePartId;
            FileName = command.FormFile.FileName;
            StorageFileName = storageFileName;
            ContentType = command.FormFile.ContentType;
            FileSize = command.FormFile.Length;
            Description = command.Description;
        }

        [JsonConstructor]
        protected MusicPieceFile()
        {
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Reference to the music piece
        /// </summary>
        public Guid MusicPieceId { get; private set; }
        public virtual MusicPiece MusicPiece { get; private set; }

        /// <summary>
        /// Optional reference to a specific part (null means it's a general file for the whole piece)
        /// </summary>
        public Guid? MusicPiecePartId { get; private set; }
        public virtual MusicPiecePart MusicPiecePart { get; private set; }

        /// <summary>
        /// Original filename as uploaded
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Generated unique filename for storage
        /// </summary>
        public string StorageFileName { get; private set; }

        /// <summary>
        /// MIME type of the file
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// Optional description of the file
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Role-based access control for this file
        /// </summary>
        [CascadingSoftDelete]
        public virtual ICollection<MusicPieceFileRole> Roles { get; private set; } = new HashSet<MusicPieceFileRole>();

        /// <summary>
        /// Section-based access control for this file (which instruments can see this file)
        /// </summary>
        [CascadingSoftDelete]
        public virtual ICollection<MusicPieceFileSection> Sections { get; private set; } = new HashSet<MusicPieceFileSection>();

        public override string ToString()
        {
            return FileName;
        }
    }
}

using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents a music piece within a setlist, including its position and optional notes.
    /// </summary>
    public class SetlistPiece : BaseEntity
    {
        public SetlistPiece(Guid? id, Guid setlistId, Guid musicPieceId, int sortOrder, string notes) : base(id)
        {
            SetlistId = setlistId;
            MusicPieceId = musicPieceId;
            SortOrder = sortOrder;
            Notes = notes;
        }

        protected SetlistPiece() { }

        public Guid SetlistId { get; set; }
        public virtual Setlist Setlist { get; set; }

        public Guid MusicPieceId { get; set; }
        public virtual MusicPiece MusicPiece { get; set; }

        /// <summary>
        /// Position in the setlist (for ordering).
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Optional notes for this piece in the context of this setlist.
        /// </summary>
        public string Notes { get; set; }
    }
}

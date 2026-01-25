using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents an instrument part/voice of a music piece (e.g., "1. Violine", "Horn in F")
    /// </summary>
    public class MusicPiecePart : BaseEntity
    {
        public MusicPiecePart(Guid? id, CreateMusicPiecePart.Command command) : base(id)
        {
            MusicPieceId = command.MusicPieceId;
            SectionId = command.SectionId;
            PartName = command.PartName;
            SortOrder = command.SortOrder;
        }

        [JsonConstructor]
        protected MusicPiecePart()
        {
        }

        public void Update(ModifyMusicPiecePart.Command command)
        {
            SectionId = command.SectionId;
            PartName = command.PartName;
            SortOrder = command.SortOrder;
        }

        public Guid MusicPieceId { get; private set; }
        public virtual MusicPiece MusicPiece { get; private set; }

        /// <summary>
        /// Reference to the instrument section (e.g., Violin, Horn)
        /// </summary>
        public Guid? SectionId { get; private set; }
        public virtual Section Section { get; private set; }

        /// <summary>
        /// Specific part name (e.g., "1. Violine", "Horn in F", "Continuo")
        /// </summary>
        public string PartName { get; private set; }

        /// <summary>
        /// Sort order for display purposes (score order)
        /// </summary>
        public int SortOrder { get; private set; }

        /// <summary>
        /// Files specific to this part
        /// </summary>
        [CascadingSoftDelete]
        public virtual ICollection<MusicPieceFile> Files { get; private set; } = new HashSet<MusicPieceFile>();

        public override string ToString()
        {
            return PartName ?? $"Part {SortOrder}";
        }
    }
}

using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Defines which sections (instruments) can access a specific music piece file
    /// </summary>
    public class MusicPieceFileSection : BaseEntity
    {
        public MusicPieceFileSection(Guid? id, Guid musicPieceFileId, Guid sectionId) : base(id)
        {
            MusicPieceFileId = musicPieceFileId;
            SectionId = sectionId;
        }

        public MusicPieceFileSection(MusicPieceFile musicPieceFile, Section section, Guid? id = null) : base(id)
        {
            MusicPieceFile = musicPieceFile;
            Section = section;
        }

        [JsonConstructor]
        protected MusicPieceFileSection()
        {
        }

        public Guid MusicPieceFileId { get; private set; }
        public virtual MusicPieceFile MusicPieceFile { get; private set; }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
    }
}

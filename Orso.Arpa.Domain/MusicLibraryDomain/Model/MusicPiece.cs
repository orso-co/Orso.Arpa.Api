using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    public class MusicPiece : BaseEntity
    {
        public MusicPiece(Guid? id, CreateMusicPiece.Command command) : base(id)
        {
            Title = command.Title;
            Composer = command.Composer;
            Arranger = command.Arranger;
            Subtitle = command.Subtitle;
            Duration = command.Duration;
            YearComposed = command.YearComposed;
            Opus = command.Opus;
            Instrumentation = command.Instrumentation;
            EpochId = command.EpochId;
            GenreId = command.GenreId;
            DifficultyLevelId = command.DifficultyLevelId;
            PerformanceNotes = command.PerformanceNotes;
            InternalNotes = command.InternalNotes;
            IsArchived = false;
        }

        [JsonConstructor]
        protected MusicPiece()
        {
        }

        public void Update(ModifyMusicPiece.Command command)
        {
            Title = command.Title;
            Composer = command.Composer;
            Arranger = command.Arranger;
            Subtitle = command.Subtitle;
            Duration = command.Duration;
            YearComposed = command.YearComposed;
            Opus = command.Opus;
            Instrumentation = command.Instrumentation;
            EpochId = command.EpochId;
            GenreId = command.GenreId;
            DifficultyLevelId = command.DifficultyLevelId;
            PerformanceNotes = command.PerformanceNotes;
            InternalNotes = command.InternalNotes;
        }

        public void SetArchived(bool isArchived)
        {
            IsArchived = isArchived;
        }

        // Required fields
        public string Title { get; private set; }

        // Optional metadata fields
        public string Composer { get; private set; }
        public string Arranger { get; private set; }
        public string Subtitle { get; private set; }

        /// <summary>
        /// Duration in seconds
        /// </summary>
        public int? Duration { get; private set; }
        public int? YearComposed { get; private set; }
        public string Opus { get; private set; }

        /// <summary>
        /// Free-text description of instrumentation/orchestration
        /// </summary>
        public string Instrumentation { get; private set; }

        // SelectValue references for categorization
        public Guid? EpochId { get; private set; }
        public virtual SelectValueMapping Epoch { get; private set; }

        public Guid? GenreId { get; private set; }
        public virtual SelectValueMapping Genre { get; private set; }

        public Guid? DifficultyLevelId { get; private set; }
        public virtual SelectValueMapping DifficultyLevel { get; private set; }

        // Notes
        public string PerformanceNotes { get; private set; }
        public string InternalNotes { get; private set; }

        // Soft archive flag
        public bool IsArchived { get; private set; }

        // Navigation properties
        [CascadingSoftDelete]
        public virtual ICollection<MusicPiecePart> Parts { get; private set; } = new HashSet<MusicPiecePart>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicPieceFile> Files { get; private set; } = new HashSet<MusicPieceFile>();

        public override string ToString()
        {
            var result = Title;
            if (!string.IsNullOrWhiteSpace(Composer))
            {
                result = $"{Composer}: {result}";
            }
            return result;
        }
    }
}

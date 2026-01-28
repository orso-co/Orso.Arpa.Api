using System;

namespace Orso.Arpa.Infrastructure.Presence
{
    /// <summary>
    /// Represents a user currently editing a stage setup
    /// </summary>
    public class StageSetupEditorDto
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }

        /// <summary>
        /// The musician profile ID this editor is currently moving (if any)
        /// </summary>
        public Guid? CurrentlyMovingMusicianProfileId { get; set; }
    }
}

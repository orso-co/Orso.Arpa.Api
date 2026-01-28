using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Presence
{
    public interface IStageSetupPresenceTracker
    {
        /// <summary>
        /// Adds an editor to a stage setup. Returns true if this is the first connection for this user to this setup.
        /// </summary>
        Task<bool> EditorJoined(Guid setupId, StageSetupEditorDto editor, string connectionId);

        /// <summary>
        /// Removes an editor from a stage setup. Returns true if this was the user's last connection to this setup.
        /// </summary>
        Task<bool> EditorLeft(Guid setupId, Guid userId, string connectionId);

        /// <summary>
        /// Gets all editors currently viewing/editing a stage setup.
        /// </summary>
        Task<StageSetupEditorDto[]> GetEditors(Guid setupId);

        /// <summary>
        /// Marks that an editor is currently moving a specific musician.
        /// </summary>
        Task SetCurrentlyMoving(Guid setupId, Guid userId, Guid? musicianProfileId);

        /// <summary>
        /// Gets the setup ID that a connection is currently viewing (if any).
        /// </summary>
        Task<Guid?> GetSetupIdForConnection(string connectionId);

        /// <summary>
        /// Removes a connection from all setups it might be in.
        /// </summary>
        Task<(Guid SetupId, bool WasLastConnection)?> RemoveConnectionFromAllSetups(Guid userId, string connectionId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Presence
{
    public class StageSetupPresenceTracker : IStageSetupPresenceTracker
    {
        private readonly object _lock = new();

        // SetupId -> (UserId -> (Editor, ConnectionIds))
        private readonly Dictionary<Guid, Dictionary<Guid, (StageSetupEditorDto Editor, HashSet<string> ConnectionIds)>> _setupEditors = new();

        // ConnectionId -> SetupId (for quick lookup on disconnect)
        private readonly Dictionary<string, Guid> _connectionToSetup = new();

        public Task<bool> EditorJoined(Guid setupId, StageSetupEditorDto editor, string connectionId)
        {
            var isFirstConnection = false;

            lock (_lock)
            {
                // Remove from any previous setup
                if (_connectionToSetup.TryGetValue(connectionId, out var previousSetupId) && previousSetupId != setupId)
                {
                    RemoveConnectionFromSetup(previousSetupId, editor.UserId, connectionId);
                }

                // Ensure setup exists in dictionary
                if (!_setupEditors.ContainsKey(setupId))
                {
                    _setupEditors[setupId] = new Dictionary<Guid, (StageSetupEditorDto, HashSet<string>)>();
                }

                var editors = _setupEditors[setupId];

                if (editors.TryGetValue(editor.UserId, out var existing))
                {
                    existing.ConnectionIds.Add(connectionId);
                }
                else
                {
                    editors[editor.UserId] = (editor, new HashSet<string> { connectionId });
                    isFirstConnection = true;
                }

                _connectionToSetup[connectionId] = setupId;
            }

            return Task.FromResult(isFirstConnection);
        }

        public Task<bool> EditorLeft(Guid setupId, Guid userId, string connectionId)
        {
            bool isLastConnection;

            lock (_lock)
            {
                isLastConnection = RemoveConnectionFromSetup(setupId, userId, connectionId);
                _connectionToSetup.Remove(connectionId);
            }

            return Task.FromResult(isLastConnection);
        }

        public Task<StageSetupEditorDto[]> GetEditors(Guid setupId)
        {
            StageSetupEditorDto[] editors;

            lock (_lock)
            {
                if (_setupEditors.TryGetValue(setupId, out var setupDict))
                {
                    editors = setupDict.Values
                        .Select(x => x.Editor)
                        .OrderBy(x => x.DisplayName)
                        .ToArray();
                }
                else
                {
                    editors = Array.Empty<StageSetupEditorDto>();
                }
            }

            return Task.FromResult(editors);
        }

        public Task SetCurrentlyMoving(Guid setupId, Guid userId, Guid? musicianProfileId)
        {
            lock (_lock)
            {
                if (_setupEditors.TryGetValue(setupId, out var setupDict))
                {
                    if (setupDict.TryGetValue(userId, out var existing))
                    {
                        existing.Editor.CurrentlyMovingMusicianProfileId = musicianProfileId;
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task<Guid?> GetSetupIdForConnection(string connectionId)
        {
            Guid? setupId = null;

            lock (_lock)
            {
                if (_connectionToSetup.TryGetValue(connectionId, out var id))
                {
                    setupId = id;
                }
            }

            return Task.FromResult(setupId);
        }

        public Task<(Guid SetupId, bool WasLastConnection)?> RemoveConnectionFromAllSetups(Guid userId, string connectionId)
        {
            (Guid, bool)? result = null;

            lock (_lock)
            {
                if (_connectionToSetup.TryGetValue(connectionId, out var setupId))
                {
                    var wasLastConnection = RemoveConnectionFromSetup(setupId, userId, connectionId);
                    _connectionToSetup.Remove(connectionId);
                    result = (setupId, wasLastConnection);
                }
            }

            return Task.FromResult(result);
        }

        private bool RemoveConnectionFromSetup(Guid setupId, Guid userId, string connectionId)
        {
            // Must be called within lock
            if (_setupEditors.TryGetValue(setupId, out var setupDict))
            {
                if (setupDict.TryGetValue(userId, out var existing))
                {
                    existing.ConnectionIds.Remove(connectionId);

                    if (existing.ConnectionIds.Count == 0)
                    {
                        setupDict.Remove(userId);

                        // Clean up empty setup
                        if (setupDict.Count == 0)
                        {
                            _setupEditors.Remove(setupId);
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}

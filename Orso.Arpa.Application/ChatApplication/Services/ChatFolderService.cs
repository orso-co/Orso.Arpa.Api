using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.ChatApplication.Interfaces;
using Orso.Arpa.Application.ChatApplication.Model;
using Orso.Arpa.Domain.ChatDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Application.ChatApplication.Services
{
    public class ChatFolderService : IChatFolderService
    {
        private readonly IArpaContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<ChatFolderService> _logger;

        private const int MaxNestingDepth = 3;

        public ChatFolderService(
            IArpaContext context,
            IUserAccessor userAccessor,
            ILogger<ChatFolderService> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<ChatFolderConfigDto> GetFolderConfigAsync(CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;

            // Load all system folders
            List<ChatFolder> systemFolders = await _context.ChatFolders
                .Where(f => f.IsSystem && !f.Deleted)
                .OrderBy(f => f.SortOrder)
                .ToListAsync(cancellationToken);

            // Load all personal folders for this user
            List<ChatFolder> personalFolders = await _context.ChatFolders
                .Where(f => !f.IsSystem && f.OwnerId == userId && !f.Deleted)
                .OrderBy(f => f.SortOrder)
                .ToListAsync(cancellationToken);

            // Load all room assignments relevant to this user
            List<ChatFolderRoomAssignment> assignments = await _context.ChatFolderRoomAssignments
                .Where(a => !a.Deleted && (a.UserId == null || a.UserId == userId))
                .ToListAsync(cancellationToken);

            // Build room-to-folder mapping
            var roomToFolder = new Dictionary<string, string>();
            foreach (ChatFolderRoomAssignment assignment in assignments)
            {
                string roomIdStr = assignment.ChatRoomId.ToString();
                string folderIdStr = assignment.FolderId.ToString();
                // Personal assignments override system assignments
                if (assignment.UserId != null || !roomToFolder.ContainsKey(roomIdStr))
                {
                    roomToFolder[roomIdStr] = folderIdStr;
                }
            }

            return new ChatFolderConfigDto
            {
                SystemFolders = BuildFolderTree(systemFolders, assignments.Where(a => a.UserId == null).ToList()),
                PersonalFolders = BuildFolderTree(personalFolders, assignments.Where(a => a.UserId == userId).ToList()),
                RoomToFolder = roomToFolder
            };
        }

        #region Personal Folders

        public async Task<ChatFolderDto> CreatePersonalFolderAsync(CreateChatFolderDto dto, CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;

            if (dto.ParentId.HasValue)
            {
                await ValidateNestingDepthAsync(dto.ParentId.Value, false, cancellationToken);
            }

            int maxOrder = await _context.ChatFolders
                .Where(f => !f.IsSystem && f.OwnerId == userId && f.ParentId == dto.ParentId && !f.Deleted)
                .Select(f => (int?)f.SortOrder)
                .MaxAsync(cancellationToken) ?? -1;

            var folder = new ChatFolder(null, dto.Name, isSystem: false, ownerId: userId, parentId: dto.ParentId, sortOrder: maxOrder + 1);
            folder.Create(_userAccessor.DisplayName, DateTime.UtcNow);
            _context.ChatFolders.Add(folder);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDto(folder, new List<ChatFolderRoomAssignment>());
        }

        public async Task<ChatFolderDto> UpdatePersonalFolderAsync(Guid folderId, UpdateChatFolderDto dto, CancellationToken cancellationToken = default)
        {
            ChatFolder folder = await GetOwnedFolderAsync(folderId, false, cancellationToken);

            if (dto.Name != null)
            {
                folder.UpdateName(dto.Name);
            }
            if (dto.ParentId != null)
            {
                if (dto.ParentId != Guid.Empty)
                {
                    await ValidateNestingDepthAsync(dto.ParentId.Value, false, cancellationToken);
                }
                folder.UpdateParent(dto.ParentId == Guid.Empty ? null : dto.ParentId);
            }

            folder.Modify(_userAccessor.DisplayName, DateTime.UtcNow);
            await _context.SaveChangesAsync(cancellationToken);

            List<ChatFolderRoomAssignment> assignments = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && a.UserId == _userAccessor.UserId && !a.Deleted)
                .ToListAsync(cancellationToken);

            return MapToDto(folder, assignments);
        }

        public async Task DeletePersonalFolderAsync(Guid folderId, CancellationToken cancellationToken = default)
        {
            ChatFolder folder = await GetOwnedFolderAsync(folderId, false, cancellationToken);
            folder.Delete(_userAccessor.DisplayName, DateTime.UtcNow);

            // Also soft-delete room assignments
            List<ChatFolderRoomAssignment> assignments = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && !a.Deleted)
                .ToListAsync(cancellationToken);
            foreach (ChatFolderRoomAssignment assignment in assignments)
            {
                assignment.Delete(_userAccessor.DisplayName, DateTime.UtcNow);
            }

            // Soft-delete child folders recursively
            await DeleteChildFoldersAsync(folderId, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ReorderPersonalFoldersAsync(ReorderFoldersDto dto, CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;
            for (int i = 0; i < dto.FolderIds.Count; i++)
            {
                ChatFolder folder = await _context.ChatFolders
                    .FirstOrDefaultAsync(f => f.Id == dto.FolderIds[i] && f.OwnerId == userId && !f.IsSystem && !f.Deleted, cancellationToken);
                if (folder != null)
                {
                    folder.UpdateSortOrder(i);
                    folder.Modify(_userAccessor.DisplayName, DateTime.UtcNow);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AssignRoomToPersonalFolderAsync(Guid folderId, AssignRoomToFolderDto dto, CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;
            await GetOwnedFolderAsync(folderId, false, cancellationToken);

            // Remove existing personal assignment for this room in any folder (hard delete to avoid unique constraint issues)
            List<ChatFolderRoomAssignment> existing = await _context.Set<ChatFolderRoomAssignment>()
                .IgnoreQueryFilters()
                .Where(a => a.ChatRoomId == dto.ChatRoomId && a.UserId == userId)
                .ToListAsync(cancellationToken);
            _context.Set<ChatFolderRoomAssignment>().RemoveRange(existing);

            int maxOrder = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && a.UserId == userId && !a.Deleted)
                .Select(a => (int?)a.SortOrder)
                .MaxAsync(cancellationToken) ?? -1;

            var assignment = new ChatFolderRoomAssignment(null, folderId, dto.ChatRoomId, userId, maxOrder + 1);
            assignment.Create(_userAccessor.DisplayName, DateTime.UtcNow);
            _context.ChatFolderRoomAssignments.Add(assignment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRoomFromPersonalFolderAsync(Guid folderId, Guid roomId, CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;
            ChatFolderRoomAssignment assignment = await _context.Set<ChatFolderRoomAssignment>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.FolderId == folderId && a.ChatRoomId == roomId && a.UserId == userId, cancellationToken);

            if (assignment != null)
            {
                _context.Set<ChatFolderRoomAssignment>().Remove(assignment);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        #endregion

        #region System Folders

        public async Task<ChatFolderDto> CreateSystemFolderAsync(CreateChatFolderDto dto, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();

            if (dto.ParentId.HasValue)
            {
                await ValidateNestingDepthAsync(dto.ParentId.Value, true, cancellationToken);
            }

            int maxOrder = await _context.ChatFolders
                .Where(f => f.IsSystem && f.ParentId == dto.ParentId && !f.Deleted)
                .Select(f => (int?)f.SortOrder)
                .MaxAsync(cancellationToken) ?? -1;

            var folder = new ChatFolder(null, dto.Name, isSystem: true, ownerId: null, parentId: dto.ParentId, sortOrder: maxOrder + 1);
            folder.Create(_userAccessor.DisplayName, DateTime.UtcNow);
            _context.ChatFolders.Add(folder);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDto(folder, new List<ChatFolderRoomAssignment>());
        }

        public async Task<ChatFolderDto> UpdateSystemFolderAsync(Guid folderId, UpdateChatFolderDto dto, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();

            ChatFolder folder = await GetSystemFolderAsync(folderId, cancellationToken);

            if (dto.Name != null)
            {
                folder.UpdateName(dto.Name);
            }
            if (dto.ParentId != null)
            {
                if (dto.ParentId != Guid.Empty)
                {
                    await ValidateNestingDepthAsync(dto.ParentId.Value, true, cancellationToken);
                }
                folder.UpdateParent(dto.ParentId == Guid.Empty ? null : dto.ParentId);
            }

            folder.Modify(_userAccessor.DisplayName, DateTime.UtcNow);
            await _context.SaveChangesAsync(cancellationToken);

            List<ChatFolderRoomAssignment> assignments = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && a.UserId == null && !a.Deleted)
                .ToListAsync(cancellationToken);

            return MapToDto(folder, assignments);
        }

        public async Task DeleteSystemFolderAsync(Guid folderId, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();

            ChatFolder folder = await GetSystemFolderAsync(folderId, cancellationToken);
            folder.Delete(_userAccessor.DisplayName, DateTime.UtcNow);

            // Soft-delete room assignments
            List<ChatFolderRoomAssignment> assignments = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && !a.Deleted)
                .ToListAsync(cancellationToken);
            foreach (ChatFolderRoomAssignment assignment in assignments)
            {
                assignment.Delete(_userAccessor.DisplayName, DateTime.UtcNow);
            }

            await DeleteChildFoldersAsync(folderId, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ReorderSystemFoldersAsync(ReorderFoldersDto dto, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();

            for (int i = 0; i < dto.FolderIds.Count; i++)
            {
                ChatFolder folder = await _context.ChatFolders
                    .FirstOrDefaultAsync(f => f.Id == dto.FolderIds[i] && f.IsSystem && !f.Deleted, cancellationToken);
                if (folder != null)
                {
                    folder.UpdateSortOrder(i);
                    folder.Modify(_userAccessor.DisplayName, DateTime.UtcNow);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AssignRoomToSystemFolderAsync(Guid folderId, AssignRoomToFolderDto dto, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();
            await GetSystemFolderAsync(folderId, cancellationToken);

            // Remove existing system assignments for this room in ANY system folder (not just target)
            List<ChatFolderRoomAssignment> existingAll = await _context.Set<ChatFolderRoomAssignment>()
                .IgnoreQueryFilters()
                .Where(a => a.ChatRoomId == dto.ChatRoomId && a.UserId == null)
                .ToListAsync(cancellationToken);

            _context.Set<ChatFolderRoomAssignment>().RemoveRange(existingAll);

            int maxOrder = await _context.ChatFolderRoomAssignments
                .Where(a => a.FolderId == folderId && a.UserId == null && !a.Deleted)
                .Select(a => (int?)a.SortOrder)
                .MaxAsync(cancellationToken) ?? -1;

            var assignment = new ChatFolderRoomAssignment(null, folderId, dto.ChatRoomId, userId: null, maxOrder + 1);
            assignment.Create(_userAccessor.DisplayName, DateTime.UtcNow);
            _context.ChatFolderRoomAssignments.Add(assignment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRoomFromSystemFolderAsync(Guid folderId, Guid roomId, CancellationToken cancellationToken = default)
        {
            EnsureStaffOrAdmin();

            ChatFolderRoomAssignment assignment = await _context.Set<ChatFolderRoomAssignment>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.FolderId == folderId && a.ChatRoomId == roomId && a.UserId == null, cancellationToken);

            if (assignment != null)
            {
                _context.Set<ChatFolderRoomAssignment>().Remove(assignment);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        #endregion

        #region Migration

        public async Task MigrateFromLocalStorageAsync(MigrateFoldersDto dto, CancellationToken cancellationToken = default)
        {
            Guid userId = _userAccessor.UserId;

            // Check if user already has personal folders (migration already done)
            bool hasFolders = await _context.ChatFolders
                .AnyAsync(f => !f.IsSystem && f.OwnerId == userId && !f.Deleted, cancellationToken);
            if (hasFolders)
            {
                _logger.LogInformation("User {UserId} already has personal folders, skipping migration", userId);
                return;
            }

            // Map old folder IDs to new folder IDs
            var idMapping = new Dictionary<string, Guid>();

            foreach (MigrateFolderItemDto folderItem in dto.Folders.OrderBy(f => f.Order))
            {
                var folder = new ChatFolder(null, folderItem.Name, isSystem: false, ownerId: userId, parentId: null, sortOrder: folderItem.Order);
                folder.Create(_userAccessor.DisplayName, DateTime.UtcNow);
                _context.ChatFolders.Add(folder);
                idMapping[folderItem.Id] = folder.Id;
            }

            foreach (KeyValuePair<string, string> mapping in dto.RoomToFolder)
            {
                if (Guid.TryParse(mapping.Key, out Guid roomId) && idMapping.TryGetValue(mapping.Value, out Guid folderId))
                {
                    var assignment = new ChatFolderRoomAssignment(null, folderId, roomId, userId, sortOrder: 0);
                    assignment.Create(_userAccessor.DisplayName, DateTime.UtcNow);
                    _context.ChatFolderRoomAssignments.Add(assignment);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Migrated {FolderCount} folders and {AssignmentCount} room assignments for user {UserId}",
                dto.Folders.Count, dto.RoomToFolder.Count, userId);
        }

        #endregion

        #region Private Helpers

        private List<ChatFolderDto> BuildFolderTree(List<ChatFolder> folders, List<ChatFolderRoomAssignment> assignments)
        {
            var folderDtos = folders.Select(f => MapToDto(f, assignments.Where(a => a.FolderId == f.Id).ToList())).ToList();

            // Build tree from flat list
            var lookup = folderDtos.ToDictionary(f => f.Id);
            var roots = new List<ChatFolderDto>();

            foreach (ChatFolderDto dto in folderDtos)
            {
                if (dto.ParentId.HasValue && lookup.TryGetValue(dto.ParentId.Value, out ChatFolderDto parent))
                {
                    parent.Children.Add(dto);
                }
                else
                {
                    roots.Add(dto);
                }
            }

            return roots.OrderBy(f => f.SortOrder).ToList();
        }

        private static ChatFolderDto MapToDto(ChatFolder folder, List<ChatFolderRoomAssignment> assignments)
        {
            return new ChatFolderDto
            {
                Id = folder.Id,
                Name = folder.Name,
                IsSystem = folder.IsSystem,
                ParentId = folder.ParentId,
                SortOrder = folder.SortOrder,
                RoomIds = assignments
                    .Where(a => a.FolderId == folder.Id)
                    .OrderBy(a => a.SortOrder)
                    .Select(a => a.ChatRoomId)
                    .ToList()
            };
        }

        private async Task<ChatFolder> GetOwnedFolderAsync(Guid folderId, bool isSystem, CancellationToken cancellationToken)
        {
            Guid userId = _userAccessor.UserId;
            ChatFolder folder = await _context.ChatFolders
                .FirstOrDefaultAsync(f => f.Id == folderId && !f.IsSystem && f.OwnerId == userId && !f.Deleted, cancellationToken);

            if (folder == null)
            {
                throw new UnauthorizedAccessException($"Folder {folderId} not found or not owned by current user");
            }
            return folder;
        }

        private async Task<ChatFolder> GetSystemFolderAsync(Guid folderId, CancellationToken cancellationToken)
        {
            ChatFolder folder = await _context.ChatFolders
                .FirstOrDefaultAsync(f => f.Id == folderId && f.IsSystem && !f.Deleted, cancellationToken);

            if (folder == null)
            {
                throw new KeyNotFoundException($"System folder {folderId} not found");
            }
            return folder;
        }

        private async Task ValidateNestingDepthAsync(Guid parentId, bool isSystem, CancellationToken cancellationToken)
        {
            int depth = 1;
            Guid? currentParentId = parentId;

            while (currentParentId.HasValue)
            {
                depth++;
                if (depth > MaxNestingDepth)
                {
                    throw new InvalidOperationException($"Maximum nesting depth of {MaxNestingDepth} exceeded");
                }

                ChatFolder parent = await _context.ChatFolders
                    .FirstOrDefaultAsync(f => f.Id == currentParentId.Value && !f.Deleted, cancellationToken);

                currentParentId = parent?.ParentId;
            }
        }

        private async Task DeleteChildFoldersAsync(Guid parentId, CancellationToken cancellationToken)
        {
            List<ChatFolder> children = await _context.ChatFolders
                .Where(f => f.ParentId == parentId && !f.Deleted)
                .ToListAsync(cancellationToken);

            foreach (ChatFolder child in children)
            {
                child.Delete(_userAccessor.DisplayName, DateTime.UtcNow);

                List<ChatFolderRoomAssignment> childAssignments = await _context.ChatFolderRoomAssignments
                    .Where(a => a.FolderId == child.Id && !a.Deleted)
                    .ToListAsync(cancellationToken);
                foreach (ChatFolderRoomAssignment assignment in childAssignments)
                {
                    assignment.Delete(_userAccessor.DisplayName, DateTime.UtcNow);
                }

                await DeleteChildFoldersAsync(child.Id, cancellationToken);
            }
        }

        private void EnsureStaffOrAdmin()
        {
            IList<string> roles = _userAccessor.GetUserRoles();
            if (!roles.Contains(RoleNames.Staff) && !roles.Contains(RoleNames.Admin))
            {
                throw new UnauthorizedAccessException("Only staff or admin users can manage system folders");
            }
        }

        #endregion
    }
}

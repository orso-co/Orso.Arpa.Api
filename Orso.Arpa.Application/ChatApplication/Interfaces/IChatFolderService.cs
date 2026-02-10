using System;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.ChatApplication.Model;

namespace Orso.Arpa.Application.ChatApplication.Interfaces
{
    public interface IChatFolderService
    {
        Task<ChatFolderConfigDto> GetFolderConfigAsync(CancellationToken cancellationToken = default);

        // Personal folder CRUD
        Task<ChatFolderDto> CreatePersonalFolderAsync(CreateChatFolderDto dto, CancellationToken cancellationToken = default);
        Task<ChatFolderDto> UpdatePersonalFolderAsync(Guid folderId, UpdateChatFolderDto dto, CancellationToken cancellationToken = default);
        Task DeletePersonalFolderAsync(Guid folderId, CancellationToken cancellationToken = default);
        Task ReorderPersonalFoldersAsync(ReorderFoldersDto dto, CancellationToken cancellationToken = default);

        // Personal room assignments
        Task AssignRoomToPersonalFolderAsync(Guid folderId, AssignRoomToFolderDto dto, CancellationToken cancellationToken = default);
        Task RemoveRoomFromPersonalFolderAsync(Guid folderId, Guid roomId, CancellationToken cancellationToken = default);

        // System folder CRUD (staff/admin only)
        Task<ChatFolderDto> CreateSystemFolderAsync(CreateChatFolderDto dto, CancellationToken cancellationToken = default);
        Task<ChatFolderDto> UpdateSystemFolderAsync(Guid folderId, UpdateChatFolderDto dto, CancellationToken cancellationToken = default);
        Task DeleteSystemFolderAsync(Guid folderId, CancellationToken cancellationToken = default);
        Task ReorderSystemFoldersAsync(ReorderFoldersDto dto, CancellationToken cancellationToken = default);

        // System room assignments (staff/admin only)
        Task AssignRoomToSystemFolderAsync(Guid folderId, AssignRoomToFolderDto dto, CancellationToken cancellationToken = default);
        Task RemoveRoomFromSystemFolderAsync(Guid folderId, Guid roomId, CancellationToken cancellationToken = default);

        // Migration from localStorage
        Task MigrateFromLocalStorageAsync(MigrateFoldersDto dto, CancellationToken cancellationToken = default);
    }
}

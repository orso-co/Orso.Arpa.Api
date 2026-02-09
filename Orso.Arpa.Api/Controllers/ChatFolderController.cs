using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.ChatApplication.Interfaces;
using Orso.Arpa.Application.ChatApplication.Model;

namespace Orso.Arpa.Api.Controllers
{
    public class ChatFolderController : BaseController
    {
        private readonly IChatFolderService _chatFolderService;

        public ChatFolderController(IChatFolderService chatFolderService)
        {
            _chatFolderService = chatFolderService;
        }

        /// <summary>
        /// Gets the full folder configuration (system + personal folders, room mappings).
        /// </summary>
        [HttpGet("config")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatFolderConfigDto>> GetConfig()
        {
            return Ok(await _chatFolderService.GetFolderConfigAsync());
        }

        #region Personal Folders

        /// <summary>
        /// Creates a personal folder for the current user.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatFolderDto>> CreatePersonalFolder([FromBody] CreateChatFolderDto dto)
        {
            return Ok(await _chatFolderService.CreatePersonalFolderAsync(dto));
        }

        /// <summary>
        /// Updates a personal folder.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatFolderDto>> UpdatePersonalFolder(Guid id, [FromBody] UpdateChatFolderDto dto)
        {
            return Ok(await _chatFolderService.UpdatePersonalFolderAsync(id, dto));
        }

        /// <summary>
        /// Deletes a personal folder.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePersonalFolder(Guid id)
        {
            await _chatFolderService.DeletePersonalFolderAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Reorders personal folders.
        /// </summary>
        [HttpPost("reorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ReorderPersonalFolders([FromBody] ReorderFoldersDto dto)
        {
            await _chatFolderService.ReorderPersonalFoldersAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Assigns a room to a personal folder.
        /// </summary>
        [HttpPost("{id}/rooms")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignRoomToPersonalFolder(Guid id, [FromBody] AssignRoomToFolderDto dto)
        {
            await _chatFolderService.AssignRoomToPersonalFolderAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Removes a room from a personal folder.
        /// </summary>
        [HttpDelete("{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveRoomFromPersonalFolder(Guid id, Guid roomId)
        {
            await _chatFolderService.RemoveRoomFromPersonalFolderAsync(id, roomId);
            return NoContent();
        }

        #endregion

        #region System Folders

        /// <summary>
        /// Creates a system folder (staff/admin only).
        /// </summary>
        [HttpPost("system")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChatFolderDto>> CreateSystemFolder([FromBody] CreateChatFolderDto dto)
        {
            return Ok(await _chatFolderService.CreateSystemFolderAsync(dto));
        }

        /// <summary>
        /// Updates a system folder (staff/admin only).
        /// </summary>
        [HttpPut("system/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChatFolderDto>> UpdateSystemFolder(Guid id, [FromBody] UpdateChatFolderDto dto)
        {
            return Ok(await _chatFolderService.UpdateSystemFolderAsync(id, dto));
        }

        /// <summary>
        /// Deletes a system folder (staff/admin only).
        /// </summary>
        [HttpDelete("system/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteSystemFolder(Guid id)
        {
            await _chatFolderService.DeleteSystemFolderAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Reorders system folders (staff/admin only).
        /// </summary>
        [HttpPost("system/reorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ReorderSystemFolders([FromBody] ReorderFoldersDto dto)
        {
            await _chatFolderService.ReorderSystemFoldersAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Assigns a room to a system folder (staff/admin only).
        /// </summary>
        [HttpPost("system/{id}/rooms")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AssignRoomToSystemFolder(Guid id, [FromBody] AssignRoomToFolderDto dto)
        {
            await _chatFolderService.AssignRoomToSystemFolderAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Removes a room from a system folder (staff/admin only).
        /// </summary>
        [HttpDelete("system/{id}/rooms/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveRoomFromSystemFolder(Guid id, Guid roomId)
        {
            await _chatFolderService.RemoveRoomFromSystemFolderAsync(id, roomId);
            return NoContent();
        }

        #endregion

        #region Migration

        /// <summary>
        /// Migrates folder data from localStorage to the server.
        /// </summary>
        [HttpPost("migrate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MigrateFromLocalStorage([FromBody] MigrateFoldersDto dto)
        {
            await _chatFolderService.MigrateFromLocalStorageAsync(dto);
            return NoContent();
        }

        #endregion
    }
}

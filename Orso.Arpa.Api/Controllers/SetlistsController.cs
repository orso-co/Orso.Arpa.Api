using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.SetlistApplication.Model;
using Orso.Arpa.Application.SetlistApplication.Services;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class SetlistsController : BaseController
    {
        private readonly ISetlistService _setlistService;

        public SetlistsController(ISetlistService setlistService)
        {
            _setlistService = setlistService;
        }

        /// <summary>
        /// Gets all setlists.
        /// </summary>
        /// <param name="includeTemplates">Include template setlists (default: true)</param>
        /// <returns>List of setlists</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SetlistDto>>> Get([FromQuery] bool includeTemplates = true)
        {
            return Ok(await _setlistService.GetAsync(includeTemplates));
        }

        /// <summary>
        /// Gets a setlist by id.
        /// </summary>
        /// <param name="id">Setlist id</param>
        /// <returns>The setlist</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SetlistDto>> GetById(Guid id)
        {
            return Ok(await _setlistService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new setlist.
        /// </summary>
        /// <param name="createDto">Setlist data</param>
        /// <returns>The created setlist</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetlistDto>> Post([FromBody] SetlistCreateDto createDto)
        {
            SetlistDto result = await _setlistService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing setlist.
        /// </summary>
        /// <param name="modifyDto">Modified setlist data</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(SetlistModifyDto modifyDto)
        {
            await _setlistService.ModifyAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a setlist.
        /// </summary>
        /// <param name="id">Setlist id</param>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _setlistService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Adds a music piece to a setlist.
        /// </summary>
        /// <param name="id">Setlist id</param>
        /// <param name="dto">Piece data</param>
        /// <returns>The added setlist piece</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/pieces")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SetlistPieceDto>> AddPiece(Guid id, [FromBody] AddPieceToSetlistDto dto)
        {
            SetlistPieceDto result = await _setlistService.AddPieceAsync(id, dto);
            return CreatedAtAction(nameof(GetById), new { id = id }, result);
        }

        /// <summary>
        /// Removes a music piece from a setlist.
        /// </summary>
        /// <param name="id">Setlist id</param>
        /// <param name="pieceId">Setlist piece id</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/pieces/{pieceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemovePiece(Guid id, Guid pieceId)
        {
            await _setlistService.RemovePieceAsync(id, pieceId);
            return NoContent();
        }

        /// <summary>
        /// Reorders pieces in a setlist.
        /// </summary>
        /// <param name="id">Setlist id</param>
        /// <param name="orderedPieceIds">List of setlist piece IDs in desired order</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/pieces/reorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReorderPieces(Guid id, [FromBody] List<Guid> orderedPieceIds)
        {
            await _setlistService.ReorderPiecesAsync(id, orderedPieceIds);
            return NoContent();
        }
    }
}

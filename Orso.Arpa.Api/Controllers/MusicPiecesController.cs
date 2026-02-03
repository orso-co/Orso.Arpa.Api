using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.MusicPieceApplication.Interfaces;
using Orso.Arpa.Application.MusicPieceApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class MusicPiecesController : BaseController
    {
        private readonly IMusicPieceService _musicPieceService;

        public MusicPiecesController(IMusicPieceService musicPieceService)
        {
            _musicPieceService = musicPieceService;
        }

        /// <summary>
        /// Gets all music pieces
        /// </summary>
        /// <param name="includeArchived">Include archived pieces</param>
        /// <returns>List of music pieces</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MusicPieceDto>>> Get([FromQuery] bool includeArchived = false)
        {
            return Ok(await _musicPieceService.GetAsync(includeArchived));
        }

        /// <summary>
        /// Gets a music piece by id
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <returns>The music piece</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MusicPieceDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _musicPieceService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new music piece
        /// </summary>
        /// <param name="createDto">Music piece data</param>
        /// <returns>The created music piece</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicPieceDto>> Post([FromBody] MusicPieceCreateDto createDto)
        {
            MusicPieceDto created = await _musicPieceService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing music piece
        /// </summary>
        /// <param name="modifyDto">Modified music piece data</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(MusicPieceModifyDto modifyDto)
        {
            await _musicPieceService.ModifyAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _musicPieceService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Archives or unarchives a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="isArchived">Archive status</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SetArchived([FromRoute] Guid id, [FromQuery] bool isArchived)
        {
            await _musicPieceService.SetArchivedAsync(id, isArchived);
            return NoContent();
        }

        /// <summary>
        /// Uploads a file to a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="partId">Optional part id</param>
        /// <param name="file">File to upload</param>
        /// <param name="description">Optional file description</param>
        /// <returns>The uploaded file metadata</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/files")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicPieceFileDto>> UploadFile(
            [FromRoute] Guid id,
            [FromQuery] Guid? partId,
            IFormFile file,
            [FromQuery] string description)
        {
            MusicPieceFileDto uploaded = await _musicPieceService.UploadFileAsync(id, partId, file, description);
            return CreatedAtAction(nameof(DownloadFile), new { id, fileId = uploaded.Id }, uploaded);
        }

        /// <summary>
        /// Downloads a music piece file
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="fileId">File id</param>
        /// <returns>The file content</returns>
        [Authorize(Roles = RoleNames.Staff + "," + RoleNames.Performer)]
        [HttpGet("{id}/files/{fileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DownloadFile([FromRoute] Guid id, [FromRoute] Guid fileId)
        {
            var (content, contentType, fileName) = await _musicPieceService.DownloadFileAsync(fileId);
            return File(content, contentType, fileName);
        }

        /// <summary>
        /// Deletes a music piece file
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="fileId">File id</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/files/{fileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteFile([FromRoute] Guid id, [FromRoute] Guid fileId)
        {
            await _musicPieceService.DeleteFileAsync(fileId);
            return NoContent();
        }

        /// <summary>
        /// Adds a section to a music piece file
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="fileId">File id</param>
        /// <param name="sectionId">Section id to add</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/files/{fileId}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> AddSectionToFile([FromRoute] Guid id, [FromRoute] Guid fileId, [FromRoute] Guid sectionId)
        {
            await _musicPieceService.AddSectionToFileAsync(fileId, sectionId);
            return NoContent();
        }

        /// <summary>
        /// Removes a section from a music piece file
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="fileId">File id</param>
        /// <param name="sectionId">Section id to remove</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/files/{fileId}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveSectionFromFile([FromRoute] Guid id, [FromRoute] Guid fileId, [FromRoute] Guid sectionId)
        {
            await _musicPieceService.RemoveSectionFromFileAsync(fileId, sectionId);
            return NoContent();
        }

        /// <summary>
        /// Auto-assigns sections to files based on filename patterns
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="dryRun">If true, only analyzes without making changes</param>
        /// <returns>Summary of assignments</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/files/auto-assign-sections")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AutoAssignSectionsResultDto>> AutoAssignSections([FromRoute] Guid id, [FromQuery] bool dryRun = false)
        {
            return Ok(await _musicPieceService.AutoAssignSectionsAsync(id, dryRun));
        }

        /// <summary>
        /// Auto-assigns sections to ALL music piece files based on filename patterns
        /// </summary>
        /// <param name="dryRun">If true, only analyzes without making changes</param>
        /// <returns>Summary of assignments</returns>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("files/auto-assign-sections")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AutoAssignSectionsResultDto>> AutoAssignSectionsAll([FromQuery] bool dryRun = false)
        {
            return Ok(await _musicPieceService.AutoAssignSectionsAsync(null, dryRun));
        }

        /// <summary>
        /// Adds a URL to a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="createDto">URL data</param>
        /// <returns>The created URL</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/urls")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicPieceUrlDto>> AddUrl([FromRoute] Guid id, [FromBody] MusicPieceUrlCreateDto createDto)
        {
            MusicPieceUrlDto created = await _musicPieceService.AddUrlAsync(id, createDto);
            return CreatedAtAction(nameof(GetById), new { id }, created);
        }

        /// <summary>
        /// Updates an existing URL
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="urlId">URL id</param>
        /// <param name="modifyBodyDto">Modified URL data</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/urls/{urlId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> ModifyUrl([FromRoute] Guid id, [FromRoute] Guid urlId, [FromBody] MusicPieceUrlModifyBodyDto modifyBodyDto)
        {
            var modifyDto = new MusicPieceUrlModifyDto { Id = urlId, Body = modifyBodyDto };
            await _musicPieceService.ModifyUrlAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Removes a URL from a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="urlId">URL id</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/urls/{urlId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveUrl([FromRoute] Guid id, [FromRoute] Guid urlId)
        {
            await _musicPieceService.RemoveUrlAsync(urlId);
            return NoContent();
        }

        #region Todos

        /// <summary>
        /// Adds a todo to a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="createDto">Todo data</param>
        /// <returns>The created todo</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/todos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicPieceTodoDto>> AddTodo([FromRoute] Guid id, [FromBody] MusicPieceTodoCreateDto createDto)
        {
            MusicPieceTodoDto created = await _musicPieceService.AddTodoAsync(id, createDto);
            return CreatedAtAction(nameof(GetById), new { id }, created);
        }

        /// <summary>
        /// Updates an existing todo
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="todoId">Todo id</param>
        /// <param name="modifyDto">Modified todo data</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}/todos/{todoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> ModifyTodo([FromRoute] Guid id, [FromRoute] Guid todoId, [FromBody] MusicPieceTodoModifyDto modifyDto)
        {
            await _musicPieceService.ModifyTodoAsync(todoId, modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Toggles the completion status of a todo
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="todoId">Todo id</param>
        /// <returns>The new completion status</returns>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPatch("{id}/todos/{todoId}/toggle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> ToggleTodoCompletion([FromRoute] Guid id, [FromRoute] Guid todoId)
        {
            bool isCompleted = await _musicPieceService.ToggleTodoCompletionAsync(todoId);
            return Ok(isCompleted);
        }

        /// <summary>
        /// Removes a todo from a music piece
        /// </summary>
        /// <param name="id">Music piece id</param>
        /// <param name="todoId">Todo id</param>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/todos/{todoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveTodo([FromRoute] Guid id, [FromRoute] Guid todoId)
        {
            await _musicPieceService.RemoveTodoAsync(todoId);
            return NoContent();
        }

        #endregion
    }
}

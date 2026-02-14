using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.TodoApplication.Interfaces;
using Orso.Arpa.Application.TodoApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class TodosController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly ITokenAccessor _tokenAccessor;

        public TodosController(ITodoService todoService, ITokenAccessor tokenAccessor)
        {
            _todoService = todoService;
            _tokenAccessor = tokenAccessor;
        }

        /// <summary>
        /// Gets todos with optional filters
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> Get(
            [FromQuery] TodoStatus? status,
            [FromQuery] TodoPriority? priority,
            [FromQuery] Guid? assigneeId,
            [FromQuery] TodoEntityType? entityType)
        {
            return Ok(await _todoService.GetAsync(status, priority, assigneeId, entityType));
        }

        /// <summary>
        /// Gets the current user's todos (assigned + created, not done)
        /// </summary>
        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetMyTodos()
        {
            return Ok(await _todoService.GetMyTodosAsync(_tokenAccessor.UserId));
        }

        /// <summary>
        /// Gets a todo item by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _todoService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates a new todo item
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<TodoItemDto>> Post([FromBody] TodoCreateDto dto)
        {
            TodoItemDto created = await _todoService.CreateAsync(dto, _tokenAccessor.UserId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates a todo item
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] TodoModifyDto dto)
        {
            dto.Id = id;
            await _todoService.ModifyAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Updates only the status of a todo item
        /// </summary>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchStatus([FromRoute] Guid id, [FromBody] TodoStatusUpdateDto dto)
        {
            await _todoService.UpdateStatusAsync(id, dto.Status);
            return NoContent();
        }

        /// <summary>
        /// Updates only the assignee of a todo item
        /// </summary>
        [HttpPatch("{id}/assign")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchAssign([FromRoute] Guid id, [FromBody] TodoAssignDto dto)
        {
            await _todoService.AssignAsync(id, dto.AssigneeId);
            return NoContent();
        }

        /// <summary>
        /// Soft deletes a todo item
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _todoService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gets todos for a specific entity
        /// </summary>
        [HttpGet("entity/{entityType}/{entityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetByEntity(
            [FromRoute] TodoEntityType entityType,
            [FromRoute] Guid entityId)
        {
            return Ok(await _todoService.GetByEntityAsync(entityType, entityId));
        }

        /// <summary>
        /// Creates a todo from a chat message
        /// </summary>
        [HttpPost("from-chat")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<TodoItemDto>> CreateFromChat([FromBody] TodoCreateDto dto, [FromQuery] Guid chatMessageId)
        {
            TodoItemDto created = await _todoService.CreateFromChatMessageAsync(dto, _tokenAccessor.UserId, chatMessageId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Gets comments for a todo item
        /// </summary>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoCommentDto>>> GetComments([FromRoute] Guid id)
        {
            return Ok(await _todoService.GetCommentsAsync(id));
        }

        /// <summary>
        /// Adds a comment to a todo item
        /// </summary>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoCommentDto>> PostComment([FromRoute] Guid id, [FromBody] TodoCommentCreateDto dto)
        {
            TodoCommentDto created = await _todoService.CreateCommentAsync(id, dto, _tokenAccessor.UserId);
            return Created(string.Empty, created);
        }
    }
}

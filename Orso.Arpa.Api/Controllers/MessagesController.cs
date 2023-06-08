using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Application.MessageApplication;

namespace Orso.Arpa.Api.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Gets a message by id
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDto>> GetById([FromRoute] Guid id)
        {
            return await _messageService.GetByIdAsync(id);
        }

        // Get all messages
        [Authorize(Roles = RoleNames.Performer)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MessageDto>>> Get()
        {
            return Ok(await _messageService.GetAsync());
        }

        // Creates a new message
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MessageDto>> Post([FromBody] MessageCreateDto createDto)
        {
            MessageDto createdDto = await _messageService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new{ id = createdDto.Id }, createdDto);
        }

        // Modifies existing message
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put(MessageModifyDto modifyDto)
        {
            await _messageService.ModifyAsync(modifyDto);

            return NoContent();

        }

        //Deletes existing message by id
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _messageService.DeleteAsync(id);

            return NoContent();
        }

    }

}


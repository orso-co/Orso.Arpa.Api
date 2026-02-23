using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.TicketApplication.Interfaces;
using Orso.Arpa.Application.TicketApplication.Model;

namespace Orso.Arpa.Api.Controllers
{
    public class TicketsController : BaseController
    {
        private readonly ITicketService _ticketService;

        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt",
            ".mp3", ".wav", ".ogg", ".m4a",
            ".mp4", ".webm", ".mov",
            ".zip", ".rar", ".7z"
        };

        private const long MaxFileSize = 10L * 1024 * 1024; // 10 MB

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [AllowAnonymous]
        [HttpPost("support")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateSupportTicket([FromBody] CreateSupportTicketDto dto)
        {
            var ticketId = await _ticketService.CreateSupportTicketAsync(dto);
            return StatusCode(StatusCodes.Status201Created, new { id = ticketId });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TicketListItemDto>>> GetTickets(
            [FromQuery] string status,
            [FromQuery] string type,
            [FromQuery] Guid? creatorId,
            [FromQuery] string search)
        {
            var tickets = await _ticketService.GetTicketsAsync(status, type, creatorId, search);
            return Ok(tickets);
        }

        [HttpGet("stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketStatsDto>> GetStats()
        {
            var stats = await _ticketService.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("unread-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var count = await _ticketService.GetUnreadCountAsync();
            return Ok(count);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        [RequestSizeLimit(104_857_600)]
        [RequestFormLimits(MultipartBodyLengthLimit = 104_857_600)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketDto>> CreateTicket(
            [FromForm] string title,
            [FromForm] string description,
            [FromForm] string type,
            [FromForm] List<IFormFile> files)
        {
            var dto = new CreateTicketDto { Title = title, Description = description, Type = type };

            var fileData = await ProcessFiles(files);
            if (fileData == null) return BadRequest("Invalid file");

            var ticket = await _ticketService.CreateTicketAsync(dto, fileData);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDto>> UpdateTicket(Guid id, [FromBody] UpdateTicketDto dto)
        {
            try
            {
                var ticket = await _ticketService.UpdateTicketAsync(id, dto);
                return Ok(ticket);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPost("{ticketId:guid}/messages")]
        [RequestSizeLimit(104_857_600)]
        [RequestFormLimits(MultipartBodyLengthLimit = 104_857_600)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketMessageDto>> AddMessage(
            Guid ticketId,
            [FromForm] string content,
            [FromForm] List<IFormFile> files)
        {
            var fileData = await ProcessFiles(files);
            if (fileData == null) return BadRequest("Invalid file");

            var message = await _ticketService.AddMessageAsync(ticketId, content, fileData);
            return Ok(message);
        }

        [HttpPost("{ticketId:guid}/vote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketListItemDto>> Vote(Guid ticketId, [FromBody] VoteDto dto)
        {
            var result = await _ticketService.VoteAsync(ticketId, dto.Value);
            return Ok(result);
        }

        [HttpPost("{ticketId:guid}/links")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketLinkDto>> AddLink(Guid ticketId, [FromBody] CreateTicketLinkDto dto)
        {
            var link = await _ticketService.AddLinkAsync(ticketId, dto);
            return Ok(link);
        }

        [HttpDelete("links/{linkId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveLink(Guid linkId)
        {
            try
            {
                await _ticketService.RemoveLinkAsync(linkId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("messages/{messageId}/reactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TicketReactionDto>>> ToggleReaction(Guid messageId, [FromBody] ToggleReactionDto dto)
        {
            var reactions = await _ticketService.ToggleReactionAsync(messageId, dto.Emoji);
            return Ok(reactions);
        }

        [HttpPost("{ticketId:guid}/read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MarkAsRead(Guid ticketId)
        {
            await _ticketService.MarkAsReadAsync(ticketId);
            return NoContent();
        }

        [HttpGet("attachments/{attachmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadAttachment(Guid attachmentId)
        {
            var result = await _ticketService.GetAttachmentFileAsync(attachmentId);
            if (result == null) return NotFound();
            var (content, fileName, contentType) = result.Value;
            return File(content, contentType, fileName);
        }

        private async Task<List<(string fileName, string contentType, byte[] data)>> ProcessFiles(List<IFormFile> files)
        {
            var fileData = new List<(string fileName, string contentType, byte[] data)>();
            if (files == null || files.Count == 0) return fileData;

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                if (!AllowedExtensions.Contains(ext)) return null;
                if (file.Length > MaxFileSize) return null;

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                fileData.Add((file.FileName, file.ContentType, ms.ToArray()));
            }

            return fileData;
        }
    }
}

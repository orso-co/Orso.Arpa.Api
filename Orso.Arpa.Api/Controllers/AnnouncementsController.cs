using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Application.AnnouncementApplication.Interfaces;
using Orso.Arpa.Application.AnnouncementApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IConfiguration _configuration;

        public AnnouncementsController(IAnnouncementService announcementService, IConfiguration configuration)
        {
            _announcementService = announcementService;
            _configuration = configuration;
        }

        [HttpGet("unread")]
        public async Task<ActionResult<UnreadAnnouncementsDto>> GetUnread()
            => Ok(await _announcementService.GetUnreadAsync());

        [HttpGet("user")]
        public async Task<ActionResult<UserAnnouncementsResponseDto>> GetAllForUser()
            => Ok(await _announcementService.GetAllForUserAsync());

        [HttpGet("ticker")]
        public async Task<ActionResult<List<AnnouncementDto>>> GetTicker()
            => Ok(await _announcementService.GetTickerItemsAsync());

        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        public async Task<ActionResult<List<AnnouncementAdminDto>>> GetAll()
            => Ok(await _announcementService.GetAllAsync());

        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        public async Task<ActionResult<AnnouncementDto>> Create([FromBody] CreateAnnouncementDto dto)
        {
            var created = await _announcementService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetUnread), created);
        }

        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAnnouncementDto dto)
        {
            dto.Id = id;
            await _announcementService.UpdateAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _announcementService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id:guid}/toggle-active")]
        public async Task<IActionResult> ToggleActive([FromRoute] Guid id)
        {
            await _announcementService.ToggleActiveAsync(id);
            return NoContent();
        }

        [HttpPost("{id:guid}/read")]
        public async Task<IActionResult> MarkAsRead([FromRoute] Guid id)
        {
            await _announcementService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpPost("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _announcementService.MarkAllAsReadAsync();
            return NoContent();
        }

        [HttpPost("{id:guid}/toggle-pin")]
        public async Task<IActionResult> TogglePin([FromRoute] Guid id)
        {
            await _announcementService.ToggleTickerPinAsync(id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("deploy")]
        public async Task<ActionResult<AnnouncementDto>> Deploy([FromBody] DeployAnnouncementDto dto)
        {
            var configuredSecret = _configuration["DeployAnnouncementSecret"];
            if (string.IsNullOrEmpty(configuredSecret) || dto.Secret != configuredSecret)
            {
                return Unauthorized();
            }

            var created = await _announcementService.CreateDeployAnnouncementAsync(dto);
            return Ok(created);
        }

        [AllowAnonymous]
        [HttpPost("deploy-starting")]
        public async Task<ActionResult<AnnouncementDto>> DeployStarting([FromBody] DeployAnnouncementDto dto)
        {
            var configuredSecret = _configuration["DeployAnnouncementSecret"];
            if (string.IsNullOrEmpty(configuredSecret) || dto.Secret != configuredSecret)
            {
                return Unauthorized();
            }

            var created = await _announcementService.CreateDeployStartingAnnouncementAsync(dto);
            return Ok(created);
        }
    }
}

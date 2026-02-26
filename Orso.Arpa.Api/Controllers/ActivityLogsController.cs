using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.ActivityLogApplication.Interfaces;
using Orso.Arpa.Application.ActivityLogApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;
using System;

namespace Orso.Arpa.Api.Controllers
{
    public class ActivityLogsController : BaseController
    {
        private readonly IActivityLogService _activityLogService;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ActivityLogsController> _logger;

        public ActivityLogsController(
            IActivityLogService activityLogService,
            ITokenAccessor tokenAccessor,
            IServiceProvider serviceProvider,
            ILogger<ActivityLogsController> logger)
        {
            _activityLogService = activityLogService;
            _tokenAccessor = tokenAccessor;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet]
        public async Task<ActionResult<ActivityLogResultDto>> GetLogs([FromQuery] ActivityLogFilterDto filter)
        {
            if (_tokenAccessor.UserName != "Wolfgang")
                return Forbid();
            return Ok(await _activityLogService.GetLogsAsync(filter));
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("stats")]
        public async Task<ActionResult<ActivityLogStatsDto>> GetStats()
        {
            if (_tokenAccessor.UserName != "Wolfgang")
                return Forbid();
            return Ok(await _activityLogService.GetStatsAsync());
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("users")]
        public async Task<ActionResult<List<ActivityLogUserDto>>> GetUsers()
        {
            if (_tokenAccessor.UserName != "Wolfgang")
                return Forbid();
            return Ok(await _activityLogService.GetDistinctUsersAsync());
        }

        [HttpPost("batch")]
        public ActionResult PostBatch([FromBody] CreateActivityLogBatchDto dto)
        {
            if (dto?.Events == null || dto.Events.Count == 0)
                return Ok();

            if (dto.Events.Count > 50)
                return BadRequest("Maximum 50 events per batch");

            var userId = _tokenAccessor.UserId;
            var username = _tokenAccessor.UserName;

            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IActivityLogService>();
                    await service.CreateBatchAsync(dto.Events, userId, username);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to persist activity log batch for user {UserId}", userId);
                }
            });

            return Ok();
        }
    }
}

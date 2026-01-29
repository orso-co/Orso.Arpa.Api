using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AuditLogApplication.Interfaces;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class AuditLogsController : BaseController
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogsController(IAuditLogService AuditLogService)
        {
            _auditLogService = AuditLogService;
        }

        /// <summary>
        /// Queries a list of audit logs for a given entityId. The list is sorted decending by DateTime. In order to control
        /// the number of items to be returned, you can skip the first n entries and take m entries starting at this position.
        /// In case the entityId is not specified, all audit logs are returned.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>A list of auditLogs</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AuditLogDto>>> Get([FromQuery] Guid? entityId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            return Ok(await _auditLogService.GetAsync(entityId, skip, take));
        }

        /// <summary>
        /// Gets recent participation changes (project and appointment) with resolved names.
        /// Used for the Dashboard activity feed.
        /// </summary>
        /// <param name="take">Number of items to return (default 20)</param>
        /// <returns>A list of recent participation changes with person names, project/appointment names, and status</returns>
        /// <response code="200">Returns the list of recent participation changes</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("recent-participation-changes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RecentParticipationChangeDto>>> GetRecentParticipationChanges([FromQuery] int take = 20)
        {
            return Ok(await _auditLogService.GetRecentParticipationChangesAsync(take));
        }
    }
}

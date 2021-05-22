using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AuditLogApplication;
using Orso.Arpa.Application.Interfaces;
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
        /// Queries a list of auditLogs for a given entityId. The list is sorted decending by DateTime. In order to control
        /// the number of items to be returned, you can skip the first n entries and take m entries starting at this position.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>A list of auditLogs</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AuditLogDto>>> Get([FromQuery] Guid entityId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            return Ok(await _auditLogService.GetAsync(entityId, skip, take));
        }
    }
}

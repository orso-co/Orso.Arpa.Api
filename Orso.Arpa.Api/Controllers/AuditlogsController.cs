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
        private readonly IAuditLogservice _auditLogservice;

        public AuditLogsController(IAuditLogservice auditLogservice)
        {
            _auditLogservice = auditLogservice;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLogDto>>> Get([FromQuery] Guid entityId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            return Ok(await _auditLogservice.GetAsync(entityId, skip, take));
        }
    }
}

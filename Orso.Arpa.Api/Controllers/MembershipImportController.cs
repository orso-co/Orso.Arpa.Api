using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.MembershipImportApplication.Model;
using Orso.Arpa.Application.MembershipImportApplication.Services;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/membership-import")]
    public class MembershipImportController : BaseController
    {
        private readonly IMembershipImportService _importService;

        public MembershipImportController(IMembershipImportService importService)
        {
            _importService = importService;
        }

        /// <summary>
        /// Returns the expected CSV format with column descriptions
        /// </summary>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet("csv-format")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CsvFormatInfoDto> GetCsvFormat()
        {
            return Ok(CsvFormatInfoDto.Create());
        }

        /// <summary>
        /// Extracts CSV headers and suggests auto-mapping to system fields
        /// </summary>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("headers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CsvHeadersResponseDto> ExtractHeaders(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            using var stream = file.OpenReadStream();
            return Ok(_importService.ExtractHeaders(stream));
        }

        /// <summary>
        /// Previews a CSV membership import by matching persons
        /// </summary>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MembershipImportPreviewDto>> Preview(
            IFormFile file,
            [FromForm] string columnMappingJson,
            CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            Dictionary<string, string> columnMapping = null;
            if (!string.IsNullOrWhiteSpace(columnMappingJson))
            {
                columnMapping = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(columnMappingJson);
            }

            using var stream = file.OpenReadStream();
            var result = await _importService.PreviewAsync(stream, cancellationToken, columnMapping);
            return Ok(result);
        }

        /// <summary>
        /// Executes the membership import for confirmed rows
        /// </summary>
        /// <param name="executeDto">Import configuration with confirmed rows</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Import result summary</returns>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("execute")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MembershipImportResultDto>> Execute(
            [FromBody] MembershipImportExecuteDto executeDto,
            CancellationToken cancellationToken)
        {
            if (executeDto?.Rows == null || executeDto.Rows.Count == 0)
            {
                return BadRequest("No rows to import");
            }

            var result = await _importService.ExecuteAsync(executeDto, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Rolls back a previous import by deleting all entities created in that batch
        /// </summary>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("rollback/{importBatchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MembershipImportRollbackResultDto>> Rollback(
            Guid importBatchId,
            CancellationToken cancellationToken)
        {
            var result = await _importService.RollbackAsync(importBatchId, cancellationToken);
            if (result.MembershipsDeleted == 0 && result.PersonsDeleted == 0)
            {
                return NotFound("No import found with this batch ID");
            }
            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.SelectValueApplication.Interfaces;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/tables/{tableName}/properties/{propertyName}")]
    public class SelectValuesController : BaseController
    {
        private readonly ISelectValueService _selectValueService;

        public SelectValuesController(ISelectValueService selectValueService)
        {
            _selectValueService = selectValueService;
        }

        /// <summary>
        /// Queries select values by table name and property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tableName"></param>
        /// <returns>A list of select values</returns>
        /// <response code="200"></response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SelectValueDto>>> Get([FromRoute] string tableName, [FromRoute] string propertyName)
        {
            return Ok(await _selectValueService.GetAsync(tableName, propertyName));
        }

        /// <summary>
        /// Creates a new select value for the specified table and property
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="createBodyDto"></param>
        /// <returns>The created select value</returns>
        /// <response code="200">Returns the created select value</response>
        /// <response code="404">If the category could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<SelectValueDto>> Post(
            [FromRoute] string tableName,
            [FromRoute] string propertyName,
            [FromBody] SelectValueMappingCreateBodyDto createBodyDto)
        {
            var createDto = new SelectValueMappingCreateDto
            {
                TableName = tableName,
                PropertyName = propertyName,
                Name = createBodyDto.Name,
                Description = createBodyDto.Description
            };
            return Ok(await _selectValueService.CreateMappingAsync(createDto));
        }

        /// <summary>
        /// Updates an existing select value
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="id"></param>
        /// <param name="modifyBodyDto"></param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(
            [FromRoute] string tableName,
            [FromRoute] string propertyName,
            [FromRoute] Guid id,
            [FromBody] SelectValueMappingModifyBodyDto modifyBodyDto)
        {
            var modifyDto = new SelectValueMappingModifyDto
            {
                Id = id,
                TableName = tableName,
                PropertyName = propertyName,
                Body = modifyBodyDto
            };
            await _selectValueService.ModifyMappingAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing select value mapping
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="id"></param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromRoute] string tableName,
            [FromRoute] string propertyName,
            [FromRoute] Guid id)
        {
            await _selectValueService.DeleteMappingAsync(id);
            return NoContent();
        }
    }
}

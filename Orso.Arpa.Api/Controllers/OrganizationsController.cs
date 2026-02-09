using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.OrganizationApplication.Interfaces;
using Orso.Arpa.Application.OrganizationApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Gets all organizations
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> Get()
        {
            return Ok(await _organizationService.GetAsync());
        }

        /// <summary>
        /// Gets an organization by id
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrganizationDto>> GetById([FromRoute] Guid id)
        {
            return await _organizationService.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new organization
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OrganizationDto>> Post([FromBody] OrganizationCreateDto createDto)
        {
            OrganizationDto createdDto = await _organizationService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Modifies an existing organization
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put(OrganizationModifyDto modifyDto)
        {
            await _organizationService.ModifyAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing organization by id
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _organizationService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all persons linked to an organization
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}/persons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonOrganizationDto>>> GetPersons([FromRoute] Guid id)
        {
            return Ok(await _organizationService.GetPersonOrganizationsAsync(id));
        }

        /// <summary>
        /// Adds a person to an organization
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/persons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<PersonOrganizationDto>> AddPerson([FromRoute] Guid id, [FromBody] PersonOrganizationCreateDto createDto)
        {
            PersonOrganizationDto createdDto = await _organizationService.AddPersonAsync(id, createDto);
            return CreatedAtAction(nameof(GetPersons), new { id }, createdDto);
        }

        /// <summary>
        /// Modifies an existing person-organization link
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("person-organizations/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ModifyPersonOrganization(PersonOrganizationModifyDto modifyDto)
        {
            await _organizationService.ModifyPersonOrganizationAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Removes a person-organization link
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("person-organizations/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemovePersonOrganization([FromRoute] Guid id)
        {
            await _organizationService.RemovePersonOrganizationAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all relationships for an organization
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}/relationships")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrganizationRelationshipDto>>> GetRelationships([FromRoute] Guid id)
        {
            return Ok(await _organizationService.GetRelationshipsAsync(id));
        }

        /// <summary>
        /// Creates a new organization-organization relationship
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("relationships")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OrganizationRelationshipDto>> AddRelationship([FromBody] OrganizationRelationshipCreateDto createDto)
        {
            OrganizationRelationshipDto createdDto = await _organizationService.AddRelationshipAsync(createDto);
            return CreatedAtAction(nameof(GetRelationships), new { id = createdDto.SourceOrganizationId }, createdDto);
        }

        /// <summary>
        /// Modifies an existing organization-organization relationship
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("relationships/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ModifyRelationship(OrganizationRelationshipModifyDto modifyDto)
        {
            await _organizationService.ModifyRelationshipAsync(modifyDto);
            return NoContent();
        }

        /// <summary>
        /// Removes an organization-organization relationship
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("relationships/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveRelationship([FromRoute] Guid id)
        {
            await _organizationService.RemoveRelationshipAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all existing tags for autocomplete
        /// </summary>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetTags()
        {
            return Ok(await _organizationService.GetAllTagsAsync());
        }
    }
}

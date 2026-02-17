using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.SurveyApplication.Interfaces;
using Orso.Arpa.Application.SurveyApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers;

public class SurveysController : BaseController
{
    private readonly ISurveyService _surveyService;
    private readonly ITokenAccessor _tokenAccessor;

    public SurveysController(ISurveyService surveyService, ITokenAccessor tokenAccessor)
    {
        _surveyService = surveyService;
        _tokenAccessor = tokenAccessor;
    }

    /// <summary>
    /// Gets active surveys for the current user (for banner display)
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ActiveSurveyDto>>> GetActive()
    {
        return Ok(await _surveyService.GetActiveAsync(_tokenAccessor.UserId));
    }

    /// <summary>
    /// Gets all surveys (admin view)
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SurveyListDto>>> GetAll()
    {
        return Ok(await _surveyService.GetAllAsync());
    }

    /// <summary>
    /// Gets a survey by id with questions and options
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SurveyDto>> GetById([FromRoute] Guid id)
    {
        SurveyDto survey = await _surveyService.GetByIdAsync(id);
        return survey == null ? NotFound() : Ok(survey);
    }

    /// <summary>
    /// Creates a new survey
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<SurveyDto>> Post([FromBody] SurveyCreateDto createDto)
    {
        SurveyDto createdDto = await _surveyService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    /// <summary>
    /// Modifies an existing survey
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(SurveyModifyDto modifyDto)
    {
        await _surveyService.ModifyAsync(modifyDto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a survey
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _surveyService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Activates a survey
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate([FromRoute] Guid id)
    {
        await _surveyService.ActivateAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Deactivates a survey
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpPost("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id)
    {
        await _surveyService.DeactivateAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Submits a survey response
    /// </summary>
    [HttpPost("{id:guid}/responses")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> SubmitResponse([FromRoute] Guid id, [FromBody] SubmitSurveyResponseDto dto)
    {
        string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        await _surveyService.SubmitResponseAsync(id, _tokenAccessor.UserId, ipAddress, dto);
        return NoContent();
    }

    /// <summary>
    /// Gets the current user's response for a survey
    /// </summary>
    [HttpGet("{id:guid}/my-response")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MyResponseDto>> GetMyResponse([FromRoute] Guid id)
    {
        MyResponseDto response = await _surveyService.GetMyResponseAsync(id, _tokenAccessor.UserId);
        return Ok(response);
    }

    /// <summary>
    /// Deletes the current user's response (to retake)
    /// </summary>
    [HttpDelete("{id:guid}/my-response")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMyResponse([FromRoute] Guid id)
    {
        await _surveyService.DeleteMyResponseAsync(id, _tokenAccessor.UserId);
        return NoContent();
    }

    /// <summary>
    /// Gets aggregated survey results (admin)
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("{id:guid}/results")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SurveyResultsDto>> GetResults([FromRoute] Guid id)
    {
        return Ok(await _surveyService.GetResultsAsync(id));
    }

    /// <summary>
    /// Gets individual response details (admin)
    /// </summary>
    [Authorize(Roles = RoleNames.Staff)]
    [HttpGet("{id:guid}/responses-details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SurveyResponseDetailDto>>> GetResponseDetails([FromRoute] Guid id)
    {
        return Ok(await _surveyService.GetResponseDetailsAsync(id));
    }
}

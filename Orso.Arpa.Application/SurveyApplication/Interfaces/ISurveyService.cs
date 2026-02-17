using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.SurveyApplication.Model;

namespace Orso.Arpa.Application.SurveyApplication.Interfaces;

public interface ISurveyService
{
    Task<SurveyDto> CreateAsync(SurveyCreateDto createDto);
    Task<IEnumerable<SurveyListDto>> GetAllAsync();
    Task<IEnumerable<ActiveSurveyDto>> GetActiveAsync(Guid userId);
    Task<SurveyDto> GetByIdAsync(Guid id);
    Task ModifyAsync(SurveyModifyDto modifyDto);
    Task DeleteAsync(Guid id);
    Task ActivateAsync(Guid id);
    Task DeactivateAsync(Guid id);
    Task SubmitResponseAsync(Guid surveyId, Guid userId, string ipAddress, SubmitSurveyResponseDto dto);
    Task<MyResponseDto> GetMyResponseAsync(Guid surveyId, Guid userId);
    Task DeleteMyResponseAsync(Guid surveyId, Guid userId);
    Task<SurveyResultsDto> GetResultsAsync(Guid surveyId);
    Task<IEnumerable<SurveyResponseDetailDto>> GetResponseDetailsAsync(Guid surveyId);
}

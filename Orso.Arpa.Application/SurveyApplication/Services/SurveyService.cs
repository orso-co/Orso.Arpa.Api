using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.SurveyApplication.Interfaces;
using Orso.Arpa.Application.SurveyApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Commands;
using Orso.Arpa.Domain.SurveyDomain.Enums;
using Orso.Arpa.Domain.SurveyDomain.Model;
using Orso.Arpa.Domain.SurveyDomain.Queries;

namespace Orso.Arpa.Application.SurveyApplication.Services;

public class SurveyService :
    BaseService<
        SurveyDto,
        Survey,
        SurveyCreateDto,
        CreateSurvey.Command,
        SurveyModifyDto,
        SurveyModifyBodyDto,
        ModifySurvey.Command>, ISurveyService
{
    private readonly IArpaContext _arpaContext;

    public SurveyService(IMediator mediator, IMapper mapper, IArpaContext arpaContext) : base(mediator, mapper)
    {
        _arpaContext = arpaContext;
    }

    public new async Task<SurveyDto> CreateAsync(SurveyCreateDto createDto)
    {
        CreateSurvey.Command command = _mapper.Map<CreateSurvey.Command>(createDto);
        Survey survey = await _mediator.Send(command) as Survey;

        // Create questions and answer options after survey creation
        if (createDto.Questions != null)
        {
            foreach (QuestionCreateDto questionDto in createDto.Questions)
            {
                var question = new SurveyQuestion(
                    null,
                    survey.Id,
                    questionDto.QuestionText,
                    (QuestionType)questionDto.QuestionType,
                    questionDto.OrderIndex,
                    questionDto.IsRequired,
                    questionDto.Settings,
                    questionDto.ValidationRules);

                _ = _arpaContext.Add(question);

                if (questionDto.AnswerOptions != null)
                {
                    foreach (AnswerOptionCreateDto optionDto in questionDto.AnswerOptions)
                    {
                        var option = new SurveyAnswerOption(
                            null,
                            question.Id,
                            optionDto.OptionText,
                            optionDto.OrderIndex,
                            optionDto.Value);

                        _ = _arpaContext.Add(option);
                    }
                }
            }
            await _arpaContext.SaveChangesAsync(default);
        }

        return await GetByIdAsync(survey.Id);
    }

    public async Task<IEnumerable<SurveyListDto>> GetAllAsync()
    {
        IEnumerable<GetAllSurveys.Result> results = await _mediator.Send(new GetAllSurveys.Query());
        return results.Select(r => new SurveyListDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            IsActive = r.IsActive,
            IsAnonymous = r.IsAnonymous,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            QuestionCount = r.QuestionCount,
            ResponseCount = r.ResponseCount,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<IEnumerable<ActiveSurveyDto>> GetActiveAsync(Guid userId)
    {
        IEnumerable<GetActiveSurveys.Result> results = await _mediator.Send(new GetActiveSurveys.Query { UserId = userId });
        return results.Select(r => new ActiveSurveyDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            QuestionCount = r.QuestionCount,
            HasUserResponded = r.HasUserResponded
        });
    }

    public new async Task<SurveyDto> GetByIdAsync(Guid id)
    {
        GetSurveyById.Result result = await _mediator.Send(new GetSurveyById.Query { Id = id });
        if (result == null) return null;

        return new SurveyDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            IsActive = result.IsActive,
            IsAnonymous = result.IsAnonymous,
            Questions = result.Questions.Select(q => new SurveyQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                OrderIndex = q.OrderIndex,
                IsRequired = q.IsRequired,
                Settings = q.Settings,
                ValidationRules = q.ValidationRules,
                AnswerOptions = q.AnswerOptions.Select(a => new SurveyAnswerOptionDto
                {
                    Id = a.Id,
                    OptionText = a.OptionText,
                    OrderIndex = a.OrderIndex,
                    Value = a.Value
                }).ToList()
            }).ToList()
        };
    }

    public async Task ActivateAsync(Guid id)
    {
        await _mediator.Send(new ActivateSurvey.Command { Id = id });
    }

    public async Task DeactivateAsync(Guid id)
    {
        await _mediator.Send(new DeactivateSurvey.Command { Id = id });
    }

    public async Task SubmitResponseAsync(Guid surveyId, Guid userId, string ipAddress, SubmitSurveyResponseDto dto)
    {
        await _mediator.Send(new SubmitSurveyResponse.Command
        {
            SurveyId = surveyId,
            UserId = userId,
            IpAddress = ipAddress,
            Answers = dto.Answers.Select(a => new SubmitSurveyResponse.AnswerData
            {
                QuestionId = a.QuestionId,
                AnswerValue = a.AnswerValue
            }).ToList()
        });
    }

    public async Task<MyResponseDto> GetMyResponseAsync(Guid surveyId, Guid userId)
    {
        GetMySurveyResponse.Result result = await _mediator.Send(new GetMySurveyResponse.Query
        {
            SurveyId = surveyId,
            UserId = userId
        });

        if (result == null) return null;

        return new MyResponseDto
        {
            Id = result.Id,
            StartedAt = result.StartedAt,
            CompletedAt = result.CompletedAt,
            IsComplete = result.IsComplete,
            Answers = result.Answers.Select(a => new MyAnswerDto
            {
                QuestionId = a.QuestionId,
                AnswerValue = a.AnswerValue,
                AnsweredAt = a.AnsweredAt
            }).ToList()
        };
    }

    public async Task DeleteMyResponseAsync(Guid surveyId, Guid userId)
    {
        await _mediator.Send(new DeleteMySurveyResponse.Command
        {
            SurveyId = surveyId,
            UserId = userId
        });
    }

    public async Task<SurveyResultsDto> GetResultsAsync(Guid surveyId)
    {
        GetSurveyResults.Result result = await _mediator.Send(new GetSurveyResults.Query { SurveyId = surveyId });

        return new SurveyResultsDto
        {
            TotalResponses = result.TotalResponses,
            CompletedResponses = result.CompletedResponses,
            Questions = result.Questions.Select(q => new QuestionStatisticsDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                AnswerCount = q.AnswerCount,
                AnswerDistribution = q.AnswerDistribution,
                FreeTextAnswers = q.FreeTextAnswers,
                AverageRating = q.AverageRating
            }).ToList()
        };
    }

    public async Task<IEnumerable<SurveyResponseDetailDto>> GetResponseDetailsAsync(Guid surveyId)
    {
        IEnumerable<GetSurveyResponseDetails.Result> results = await _mediator.Send(
            new GetSurveyResponseDetails.Query { SurveyId = surveyId });

        return results.Select(r => new SurveyResponseDetailDto
        {
            ResponseId = r.ResponseId,
            UserDisplayName = r.UserDisplayName,
            UserEmail = r.UserEmail,
            StartedAt = r.StartedAt,
            CompletedAt = r.CompletedAt,
            IsComplete = r.IsComplete,
            Answers = r.Answers.Select(a => new AnswerDetailDto
            {
                QuestionId = a.QuestionId,
                QuestionText = a.QuestionText,
                AnswerValue = a.AnswerValue
            }).ToList()
        });
    }
}

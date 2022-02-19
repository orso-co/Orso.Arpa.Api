using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Logic.MyProjects;

namespace Orso.Arpa.Application.MyProjectApplication;

public class MyProjectParticipationModifyDto : IdFromRouteDto<MyProjectParticipationModifyBodyDto>
{

}

public class MyProjectParticipationModifyBodyDto
{
    public Guid ParticipationStatusId { get; set; }
    public string Comment { get; set; }
    public Guid MusicianProfileId { get; set; }
}

public class MyProjectParticipationModifyDtoMappingProfile : Profile
{
    public MyProjectParticipationModifyDtoMappingProfile()
    {
        CreateMap<MyProjectParticipationModifyDto, SetProjectParticipationStatus.Command>()
            .ForMember(dest => dest.CommentByPerformerInner,
                opt => opt.MapFrom(src => src.Body.Comment))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MusicianProfileId,
                opt => opt.MapFrom(src => src.Body.MusicianProfileId))
            .ForMember(dest => dest.ParticipationStatusInnerId,
                opt => opt.MapFrom(src => src.Body.ParticipationStatusId));
    }
}

public class MyProjectParticipationModifyDtoValidator : IdFromRouteDtoValidator<MyProjectParticipationModifyDto, MyProjectParticipationModifyBodyDto>
{
    public MyProjectParticipationModifyDtoValidator()
    {
        RuleFor(d => d.Body)
            .SetValidator(new MyProjectParticipationModifyBodyDtoValidator());
    }
}

public class MyProjectParticipationModifyBodyDtoValidator : AbstractValidator<MyProjectParticipationModifyBodyDto>
{
    public MyProjectParticipationModifyBodyDtoValidator()
    {
        RuleFor(d => d.ParticipationStatusId)
            .NotEmpty();
        RuleFor(d => d.Comment)
            .RestrictedFreeText(500);
    }
}

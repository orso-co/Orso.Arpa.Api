using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.MyProjects;

namespace Orso.Arpa.Application.MyProjectApplication;

public class MyProjectParticipationModifyDto : IdFromRouteDto<MyProjectParticipationModifyBodyDto>
{

}

public class MyProjectParticipationModifyBodyDto
{
    public ProjectParticipationStatusInner ParticipationStatusInner { get; set; }
    public string CommentByPerformerInner { get; set; }
    public Guid MusicianProfileId { get; set; }
}

public class MyProjectParticipationModifyDtoMappingProfile : Profile
{
    public MyProjectParticipationModifyDtoMappingProfile()
    {
        _ = CreateMap<MyProjectParticipationModifyDto, SetProjectParticipationStatus.Command>()
            .ForMember(dest => dest.CommentByPerformerInner,
                opt => opt.MapFrom(src => src.Body.CommentByPerformerInner))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MusicianProfileId,
                opt => opt.MapFrom(src => src.Body.MusicianProfileId))
            .ForMember(dest => dest.ParticipationStatusInner,
                opt => opt.MapFrom(src => src.Body.ParticipationStatusInner));
    }
}

public class MyProjectParticipationModifyDtoValidator : IdFromRouteDtoValidator<MyProjectParticipationModifyDto, MyProjectParticipationModifyBodyDto>
{
    public MyProjectParticipationModifyDtoValidator()
    {
        _ = RuleFor(d => d.Body)
            .SetValidator(new MyProjectParticipationModifyBodyDtoValidator());
    }
}

public class MyProjectParticipationModifyBodyDtoValidator : AbstractValidator<MyProjectParticipationModifyBodyDto>
{
    public MyProjectParticipationModifyBodyDtoValidator()
    {
        _ = RuleFor(d => d.ParticipationStatusInner)
            .IsInEnum();
        _ = RuleFor(d => d.CommentByPerformerInner)
            .RestrictedFreeText(500);
    }
}

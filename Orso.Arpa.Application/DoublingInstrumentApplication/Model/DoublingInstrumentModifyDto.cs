using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.DoublingInstrumentApplication.Model
{
    public class DoublingInstrumentModifyDto : IdFromRouteDto<DoublingInstrumentModifyBodyDto>
    {
        [FromRoute]
        public Guid DoublingInstrumentId { get; set; }
    }

    public class DoublingInstrumentModifyBodyDto
    {
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class DoublingInstrumentModifyDtoMappingProfile : Profile
    {
        public DoublingInstrumentModifyDtoMappingProfile()
        {
            CreateMap<DoublingInstrumentModifyDto, ModifyMusicianProfileSection.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoublingInstrumentId))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.Body.AvailabilityId));
        }
    }

    public class DoublingInstrumentModifyDtoValidator : IdFromRouteDtoValidator<DoublingInstrumentModifyDto, DoublingInstrumentModifyBodyDto>
    {
        public DoublingInstrumentModifyDtoValidator()
        {
            RuleFor(d => d.DoublingInstrumentId)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new DoublingInstrumentModifyBodyDtoValidator());
        }
    }

    public class DoublingInstrumentModifyBodyDtoValidator : AbstractValidator<DoublingInstrumentModifyBodyDto>
    {
        public DoublingInstrumentModifyBodyDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .RestrictedFreeText(500);

            RuleFor(dto => dto.LevelAssessmentInner)
                .FiveStarRating();

            RuleFor(dto => dto.LevelAssessmentTeam)
                .FiveStarRating();
        }
    }
}

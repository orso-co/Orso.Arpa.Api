using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MyDoublingInstrumentModifyDto : IdFromRouteDto<MyDoublingInstrumentModifyBodyDto>
    {
        [FromRoute]
        public Guid DoublingInstrumentId { get; set; }
    }

    public class MyDoublingInstrumentModifyBodyDto
    {
        public byte LevelAssessmentInner { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MyDoublingInstrumentModifyDtoMappingProfile : Profile
    {
        public MyDoublingInstrumentModifyDtoMappingProfile()
        {
            CreateMap<MyDoublingInstrumentModifyDto, Domain.Logic.Me.ModifyDoublingInstrument.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoublingInstrumentId))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id));
        }
    }

    public class MyDoublingInstrumentModifyDtoValidator : IdFromRouteDtoValidator<MyDoublingInstrumentModifyDto, MyDoublingInstrumentModifyBodyDto>
    {
        public MyDoublingInstrumentModifyDtoValidator()
        {
            RuleFor(d => d.DoublingInstrumentId)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new MyDoublingInstrumentModifyBodyDtoValidator());
        }
    }

    public class MyDoublingInstrumentModifyBodyDtoValidator : AbstractValidator<MyDoublingInstrumentModifyBodyDto>
    {
        public MyDoublingInstrumentModifyBodyDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .GeneralText(500);

            RuleFor(dto => dto.LevelAssessmentInner)
                .FiveStarRating();
        }
    }
}

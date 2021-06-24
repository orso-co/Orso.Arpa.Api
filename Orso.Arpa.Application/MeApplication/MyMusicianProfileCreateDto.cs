using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MyMusicianProfileCreateDto
    {
        public byte LevelAssessmentInner { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid? InquiryStatusInnerId { get; set; }
        public IList<MyDoublingInstrumentCreateBodyDto> DoublingInstruments { get; set; } = new List<MyDoublingInstrumentCreateBodyDto>();
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
    }

    public class MyMusicianProfileCreateDtoMappingProfile : Profile
    {
        public MyMusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileCreateDto, Create.Command>()
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))

                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.InquiryStatusInnerId))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner));
        }
    }

    public class MyMusicianProfileCreateDtoValidator : AbstractValidator<MyMusicianProfileCreateDto>
    {
        public MyMusicianProfileCreateDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating()
                .NotEqual((byte)0);

            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new MyDoublingInstrumentCreateBodyDtoValidator());

            RuleForEach(p => p.PreferredPositionsInnerIds)
              .NotEmpty();
        }
    }
}

using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MyMusicianProfileCreateDto
    {
        public byte LevelAssessmentPerformer { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public IList<MyDoublingInstrumentCreateDto> DoublingInstruments { get; set; } = new List<MyDoublingInstrumentCreateDto>();
        public IList<Guid> PreferredPositionsPerformerIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsPerformer { get; set; } = new List<byte>();
    }

    public class MyDoublingInstrumentCreateDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentPerformer { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MyMusicianProfileCreateDtoMappingProfile : Profile
    {
        public MyMusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))

                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsPerformerIds, opt => opt.MapFrom(src => src.PreferredPositionsPerformerIds))
                .ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer));

            CreateMap<MyDoublingInstrumentCreateDto, DoublingInstrumentCommand>();
        }
    }

    public class MyMusicianProfileCreateDtoValidator : AbstractValidator<MyMusicianProfileCreateDto>
    {
        public MyMusicianProfileCreateDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentPerformer)
                .FiveStarRating()
                .NotEqual((byte)0);

            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new MyDoublingInstrumentCreateDtoValidator());

            RuleForEach(p => p.PreferredPositionsPerformerIds)
              .NotEmpty();
        }
    }

    public class MyDoublingInstrumentCreateDtoValidator : AbstractValidator<MyDoublingInstrumentCreateDto>
    {
        public MyDoublingInstrumentCreateDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .MaximumLength(500);

            RuleFor(dto => dto.InstrumentId)
                .NotEmpty();

            RuleFor(dto => dto.LevelAssessmentPerformer)
                .FiveStarRating();
        }
    }
}

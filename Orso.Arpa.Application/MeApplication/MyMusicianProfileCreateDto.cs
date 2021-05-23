using System;
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
        //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        //public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
    }

    public class MyMusicianProfileCreateDtoMappingProfile : Profile
    {
        public MyMusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))

                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))

                //.ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPartsPerformer))
                ;
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

            //ToDo Validation for Collections
        }
    }

}

using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Modify;

namespace Orso.Arpa.Application.MyMusicianProfileApplication
{
    public class MyMusicianProfileModifyDto : BaseModifyDto<MyMusicianProfileModifyBodyDto>
    {
    }

    public class MyMusicianProfileModifyBodyDto
    {
        #region Native
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentPerformer { get; set; }
        public byte ProfilePreferencePerformer { get; set; }

        public string BackgroundPerformer { get; set; }
        #endregion

        #region Reference
        public Guid InstrumentId { get; set; }

        public Guid? InquiryStatusPerformerId { get; set; }
        #endregion

        #region Collection
        public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        public IList<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
        public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
        public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
        #endregion
    }

    public class MyMusicianProfileModifyDtoMappingProfile : Profile
    {
        public MyMusicianProfileModifyDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileModifyDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.Body.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.Body.LevelAssessmentPerformer))
                .ForMember(dest => dest.ProfilePreferencePerformer, opt => opt.MapFrom(src => src.Body.ProfilePreferencePerformer))
                .ForMember(dest => dest.BackgroundPerformer, opt => opt.MapFrom(src => src.Body.BackgroundPerformer))

                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.Body.InquiryStatusPerformerId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                .ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.Body.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPartsPerformer))
                ;
        }
    }

    public class MyMusicianProfileModifyDtoValidator : BaseModifyDtoValidator<MyMusicianProfileModifyDto, MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MyMusicianProfileModifyBodyDtoValidator());

        }
    }

    public class MyMusicianProfileModifyBodyDtoValidator : AbstractValidator<MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyBodyDtoValidator()
        {
            RuleFor(p => p.LevelAssessmentPerformer)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferencePerformer)
                .FiveStarRating();

            RuleFor(p => p.BackgroundPerformer)
                .MaximumLength(1000);

            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            //ToDo Validation for Collections
        }
    }
}

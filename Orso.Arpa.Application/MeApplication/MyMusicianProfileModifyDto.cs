using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Modify;

namespace Orso.Arpa.Application.MyMusicianProfileApplication
{
    public class MyMusicianProfileModifyDto : IdFromRouteDto<MyMusicianProfileModifyBodyDto>
    {
    }

    public class MyMusicianProfileModifyBodyDto
    {
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentInner { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public string BackgroundInner { get; set; }

        public Guid? InquiryStatusInnerId { get; set; }

        //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        //public IList<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
        //public IList<PreferredPosition> PreferredPositionsInner { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPart> PreferredPartsInner { get; set; } = new List<PreferredPart>();
    }

    public class MyMusicianProfileModifyDtoMappingProfile : Profile
    {
        public MyMusicianProfileModifyDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileModifyDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.Body.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.Body.BackgroundInner))

                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.Body.InquiryStatusInnerId))

                //.ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                //.ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.Body.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsInner, opt => opt.MapFrom(src => src.Body.PreferredPositionsInner))
                //.ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
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
            RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();

            RuleFor(p => p.BackgroundInner)
                .MaximumLength(1000);

            //ToDo Validation for Collections
        }
    }
}

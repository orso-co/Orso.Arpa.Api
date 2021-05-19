using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileCreateDto
    {
        #region Native
        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        #endregion

        #region Reference
        public Guid PersonId { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public Guid? InquiryStatusStaffId { get; set; }
        #endregion

        #region Collection
        public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPosition> PreferredPositionsStaff { get; set; } = new List<PreferredPosition>();
        public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
        //public IList<PreferredPart> PreferredPartsStaff { get; set; } = new HasListhSet<PreferredPart>();
        #endregion
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.LevelAssessmentStaff))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))
                .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.InquiryStatusStaffId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPositionsStaff, opt => opt.MapFrom(src => src.DoublingInstruments))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer))
                //.ForMember(dest => dest.PreferredPartsStaff, opt => opt.MapFrom(src => src.PreferredPartsStaff))
                ;
        }
    }
    public class MusicianProfileCreateDtoValidator : AbstractValidator<MusicianProfileCreateDto>
    {
        public MusicianProfileCreateDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentPerformer)
                .FiveStarRating();
            RuleFor(p => p.LevelAssessmentStaff)
                .FiveStarRating();

            RuleFor(p => p.PersonId)
               .NotEmpty();
            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            RuleFor(p => p.QualificationId)
               .NotEmpty();

            //ToDo Validation for Collections
        }
    }

}

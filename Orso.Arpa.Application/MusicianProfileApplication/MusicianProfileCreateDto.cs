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
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; set; } = new HashSet<MusicianProfileSection>();
        public virtual ICollection<PreferredPosition> PreferredPositionsPerformer { get; set; } = new HashSet<PreferredPosition>();
        //public virtual ICollection<PreferredPosition> PreferredPositionsStaff { get; set; } = new HashSet<PreferredPosition>();
        public virtual ICollection<PreferredPart> PreferredPartsPerformer { get; set; } = new HashSet<PreferredPart>();
        //public virtual ICollection<PreferredPart> PreferredPartsStaff { get; set; } = new HashSet<PreferredPart>();
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

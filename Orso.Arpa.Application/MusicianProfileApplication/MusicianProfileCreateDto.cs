using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileCreateDto : BaseModifyDto<MusicianProfileCreateBodyDto>
    {
    }

    public class MusicianProfileCreateBodyDto
    {
        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public Guid? InquiryStatusStaffId { get; set; }
        public IList<DoublingInstrumentCreateDto> DoublingInstruments { get; set; } = new List<DoublingInstrumentCreateDto>();
        public IList<Guid> PreferredPositionsPerformerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsStaffIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsPerformer { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsStaff { get; set; } = new List<byte>();
    }

    public class DoublingInstrumentCreateDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.Body.LevelAssessmentPerformer))
                .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.Body.LevelAssessmentStaff))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.Body.InquiryStatusPerformerId))
                .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.Body.InquiryStatusStaffId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsPerformerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsPerformerIds))
                .ForMember(dest => dest.PreferredPositionsStaffIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsStaffIds))
                .ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPartsPerformer))
                .ForMember(dest => dest.PreferredPartsStaff, opt => opt.MapFrom(src => src.Body.PreferredPartsStaff));

            CreateMap<DoublingInstrumentCreateDto, DoublingInstrumentCommand>();
        }
    }

    public class MusicianProfileCreateDtoValidator : BaseModifyDtoValidator<MusicianProfileCreateDto, MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileCreateBodyDtoValidator());
        }
    }

    public class MusicianProfileCreateBodyDtoValidator : AbstractValidator<MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateBodyDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentPerformer)
                .FiveStarRating();
            RuleFor(p => p.LevelAssessmentStaff)
                .FiveStarRating();

            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            RuleFor(p => p.QualificationId)
               .NotEmpty();

            RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new DoublingInstrumentCreateDtoValidator());

            RuleForEach(p => p.PreferredPositionsStaffIds)
                .NotEmpty();

            RuleForEach(p => p.PreferredPositionsPerformerIds)
                .NotEmpty();
        }
    }

    public class DoublingInstrumentCreateDtoValidator : AbstractValidator<DoublingInstrumentCreateDto>
    {
        public DoublingInstrumentCreateDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .MaximumLength(500);

            RuleFor(dto => dto.InstrumentId)
                .NotEmpty();

            RuleFor(dto => dto.LevelAssessmentPerformer)
                .FiveStarRating();

            RuleFor(dto => dto.LevelAssessmentStaff)
                .FiveStarRating();
        }
    }
}

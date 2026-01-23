using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonMembershipApplication.Model
{
    public class PersonMembershipCreateDto : IdFromRouteDto<PersonMembershipCreateBodyDto>
    {
    }

    public class PersonMembershipCreateBodyDto
    {
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal AnnualFee { get; set; }
        public Guid? SupportLevelId { get; set; }
        public Guid? MembershipStatusId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? PaymentFrequencyId { get; set; }
        public Guid? ClubId { get; set; }
        public string StaffComment { get; set; }
        public string PerformerComment { get; set; }
    }

    public class PersonMembershipCreateDtoMappingProfile : Profile
    {
        public PersonMembershipCreateDtoMappingProfile()
        {
            _ = CreateMap<PersonMembershipCreateDto, CreatePersonMembership.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.Body.EntryDate))
                .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => src.Body.ExitDate))
                .ForMember(dest => dest.AnnualFee, opt => opt.MapFrom(src => src.Body.AnnualFee))
                .ForMember(dest => dest.SupportLevelId, opt => opt.MapFrom(src => src.Body.SupportLevelId))
                .ForMember(dest => dest.MembershipStatusId, opt => opt.MapFrom(src => src.Body.MembershipStatusId))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.Body.PaymentMethodId))
                .ForMember(dest => dest.PaymentFrequencyId, opt => opt.MapFrom(src => src.Body.PaymentFrequencyId))
                .ForMember(dest => dest.ClubId, opt => opt.MapFrom(src => src.Body.ClubId))
                .ForMember(dest => dest.StaffComment, opt => opt.MapFrom(src => src.Body.StaffComment))
                .ForMember(dest => dest.PerformerComment, opt => opt.MapFrom(src => src.Body.PerformerComment));
        }
    }

    public class PersonMembershipCreateDtoValidator : IdFromRouteDtoValidator<PersonMembershipCreateDto, PersonMembershipCreateBodyDto>
    {
        public PersonMembershipCreateDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new PersonMembershipCreateBodyDtoValidator());
        }
    }

    public class PersonMembershipCreateBodyDtoValidator : AbstractValidator<PersonMembershipCreateBodyDto>
    {
        public PersonMembershipCreateBodyDtoValidator()
        {
            _ = RuleFor(c => c.EntryDate)
                .NotEmpty();

            _ = RuleFor(c => c.AnnualFee)
                .GreaterThanOrEqualTo(0);

            _ = RuleFor(c => c.StaffComment)
                .RestrictedFreeText(500);

            _ = RuleFor(c => c.PerformerComment)
                .RestrictedFreeText(500);
        }
    }
}

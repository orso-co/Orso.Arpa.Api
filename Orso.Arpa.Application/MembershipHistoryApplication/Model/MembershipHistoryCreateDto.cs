using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.MembershipHistoryApplication.Model
{
    public class MembershipHistoryCreateDto : IdFromRouteDto<MembershipHistoryCreateBodyDto>
    {
        [FromRoute]
        public Guid MembershipId { get; set; }
    }

    public class MembershipHistoryCreateBodyDto
    {
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public bool IsReduced { get; set; }
        public string Comment { get; set; }
    }

    public class MembershipHistoryCreateDtoMappingProfile : Profile
    {
        public MembershipHistoryCreateDtoMappingProfile()
        {
            _ = CreateMap<MembershipHistoryCreateDto, CreateMembershipHistory.Command>()
                .ForMember(dest => dest.PersonMembershipId, opt => opt.MapFrom(src => src.MembershipId))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Body.Year))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Body.Amount))
                .ForMember(dest => dest.IsReduced, opt => opt.MapFrom(src => src.Body.IsReduced))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment));
        }
    }

    public class MembershipHistoryCreateDtoValidator : IdFromRouteDtoValidator<MembershipHistoryCreateDto, MembershipHistoryCreateBodyDto>
    {
        public MembershipHistoryCreateDtoValidator()
        {
            _ = RuleFor(d => d.MembershipId)
                .NotEmpty();

            _ = RuleFor(d => d.Body)
                .SetValidator(new MembershipHistoryCreateBodyDtoValidator());
        }
    }

    public class MembershipHistoryCreateBodyDtoValidator : AbstractValidator<MembershipHistoryCreateBodyDto>
    {
        public MembershipHistoryCreateBodyDtoValidator()
        {
            _ = RuleFor(c => c.Year)
                .InclusiveBetween(1900, 2100);

            _ = RuleFor(c => c.Amount)
                .GreaterThanOrEqualTo(0);

            _ = RuleFor(c => c.Comment)
                .MaximumLength(500);
        }
    }
}

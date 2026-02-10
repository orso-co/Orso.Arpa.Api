using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Commands;

namespace Orso.Arpa.Application.OrganizationApplication.Model
{
    public class OrganizationModifyDto : IdFromRouteDto<OrganizationModifyBodyDto>
    {
    }

    public class OrganizationModifyBodyDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid? LegalFormId { get; set; }
        public Guid? OrganizationTypeId { get; set; }
        public IList<string> Tags { get; set; } = [];
    }

    public class OrganizationModifyDtoMappingProfile : Profile
    {
        public OrganizationModifyDtoMappingProfile()
        {
            CreateMap<OrganizationModifyDto, ModifyOrganization.Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Body.ShortName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Body.Website))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Body.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Body.Phone))
                .ForMember(dest => dest.LegalFormId, opt => opt.MapFrom(src => src.Body.LegalFormId))
                .ForMember(dest => dest.OrganizationTypeId, opt => opt.MapFrom(src => src.Body.OrganizationTypeId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.Body.Tags != null && src.Body.Tags.Count > 0
                        ? string.Join(", ", src.Body.Tags)
                        : null));
        }
    }

    public class OrganizationModifyDtoValidator : IdFromRouteDtoValidator<OrganizationModifyDto, OrganizationModifyBodyDto>
    {
        public OrganizationModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new OrganizationModifyBodyDtoValidator());
        }
    }

    public class OrganizationModifyBodyDtoValidator : AbstractValidator<OrganizationModifyBodyDto>
    {
        public OrganizationModifyBodyDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(c => c.ShortName)
                .MaximumLength(50);

            RuleFor(c => c.Description)
                .MaximumLength(1000);

            RuleFor(c => c.Website)
                .MaximumLength(500);

            RuleFor(c => c.Email)
                .MaximumLength(200)
                .EmailAddress()
                .When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Phone)
                .MaximumLength(50);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.OrganizationDomain.Commands;

namespace Orso.Arpa.Application.OrganizationApplication.Model
{
    public class OrganizationCreateDto
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

    public class OrganizationCreateDtoMappingProfile : Profile
    {
        public OrganizationCreateDtoMappingProfile()
        {
            CreateMap<OrganizationCreateDto, CreateOrganization.Command>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.Tags != null && src.Tags.Count > 0
                        ? string.Join(", ", src.Tags)
                        : null));
        }
    }

    public class OrganizationCreateDtoValidator : AbstractValidator<OrganizationCreateDto>
    {
        public OrganizationCreateDtoValidator()
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

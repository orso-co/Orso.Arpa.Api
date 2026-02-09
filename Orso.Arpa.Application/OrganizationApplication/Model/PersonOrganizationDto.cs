using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Commands;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Application.OrganizationApplication.Model
{
    public class PersonOrganizationDto : BaseEntityDto
    {
        public Guid PersonId { get; set; }
        public string PersonDisplayName { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Function { get; set; }
        public SelectValueDto RelationshipType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class PersonOrganizationDtoMappingProfile : Profile
    {
        public PersonOrganizationDtoMappingProfile()
        {
            _ = CreateMap<PersonOrganization, PersonOrganizationDto>()
                .ForMember(dest => dest.PersonDisplayName, opt => opt.MapFrom(src => src.Person.DisplayName))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }

    public class PersonOrganizationCreateDto
    {
        public Guid PersonId { get; set; }
        public string Function { get; set; }
        public Guid? RelationshipTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class PersonOrganizationCreateDtoValidator : AbstractValidator<PersonOrganizationCreateDto>
    {
        public PersonOrganizationCreateDtoValidator()
        {
            RuleFor(c => c.PersonId).NotEmpty();
            RuleFor(c => c.Function).MaximumLength(200);
        }
    }

    public class PersonOrganizationModifyDto : IdFromRouteDto<PersonOrganizationModifyBodyDto>
    {
    }

    public class PersonOrganizationModifyBodyDto
    {
        public string Function { get; set; }
        public Guid? RelationshipTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class PersonOrganizationModifyDtoMappingProfile : Profile
    {
        public PersonOrganizationModifyDtoMappingProfile()
        {
            CreateMap<PersonOrganizationModifyDto, ModifyPersonOrganization.Command>()
                .ForMember(dest => dest.Function, opt => opt.MapFrom(src => src.Body.Function))
                .ForMember(dest => dest.RelationshipTypeId, opt => opt.MapFrom(src => src.Body.RelationshipTypeId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Body.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Body.EndDate));
        }
    }

    public class PersonOrganizationModifyDtoValidator : IdFromRouteDtoValidator<PersonOrganizationModifyDto, PersonOrganizationModifyBodyDto>
    {
        public PersonOrganizationModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new PersonOrganizationModifyBodyDtoValidator());
        }
    }

    public class PersonOrganizationModifyBodyDtoValidator : AbstractValidator<PersonOrganizationModifyBodyDto>
    {
        public PersonOrganizationModifyBodyDtoValidator()
        {
            RuleFor(c => c.Function).MaximumLength(200);
        }
    }
}

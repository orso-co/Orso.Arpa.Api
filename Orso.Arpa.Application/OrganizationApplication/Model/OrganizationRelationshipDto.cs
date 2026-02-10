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
    public class OrganizationRelationshipDto : BaseEntityDto
    {
        public Guid SourceOrganizationId { get; set; }
        public string SourceOrganizationName { get; set; }
        public Guid TargetOrganizationId { get; set; }
        public string TargetOrganizationName { get; set; }
        public SelectValueDto RelationshipType { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class OrganizationRelationshipDtoMappingProfile : Profile
    {
        public OrganizationRelationshipDtoMappingProfile()
        {
            _ = CreateMap<OrganizationRelationship, OrganizationRelationshipDto>()
                .ForMember(dest => dest.SourceOrganizationName, opt => opt.MapFrom(src => src.SourceOrganization.Name))
                .ForMember(dest => dest.TargetOrganizationName, opt => opt.MapFrom(src => src.TargetOrganization.Name))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }

    public class OrganizationRelationshipCreateDto
    {
        public Guid SourceOrganizationId { get; set; }
        public Guid TargetOrganizationId { get; set; }
        public Guid? RelationshipTypeId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class OrganizationRelationshipCreateDtoMappingProfile : Profile
    {
        public OrganizationRelationshipCreateDtoMappingProfile()
        {
            CreateMap<OrganizationRelationshipCreateDto, CreateOrganizationRelationship.Command>();
        }
    }

    public class OrganizationRelationshipCreateDtoValidator : AbstractValidator<OrganizationRelationshipCreateDto>
    {
        public OrganizationRelationshipCreateDtoValidator()
        {
            RuleFor(c => c.SourceOrganizationId).NotEmpty();
            RuleFor(c => c.TargetOrganizationId).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(500);
            RuleFor(c => c.TargetOrganizationId)
                .Must((dto, targetId) => dto.SourceOrganizationId != targetId)
                .WithMessage("An organization cannot have a relationship with itself");
        }
    }

    public class OrganizationRelationshipModifyDto : IdFromRouteDto<OrganizationRelationshipModifyBodyDto>
    {
    }

    public class OrganizationRelationshipModifyBodyDto
    {
        public Guid? RelationshipTypeId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class OrganizationRelationshipModifyDtoMappingProfile : Profile
    {
        public OrganizationRelationshipModifyDtoMappingProfile()
        {
            CreateMap<OrganizationRelationshipModifyDto, ModifyOrganizationRelationship.Command>()
                .ForMember(dest => dest.RelationshipTypeId, opt => opt.MapFrom(src => src.Body.RelationshipTypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Body.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Body.EndDate));
        }
    }

    public class OrganizationRelationshipModifyDtoValidator : IdFromRouteDtoValidator<OrganizationRelationshipModifyDto, OrganizationRelationshipModifyBodyDto>
    {
        public OrganizationRelationshipModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new OrganizationRelationshipModifyBodyDtoValidator());
        }
    }

    public class OrganizationRelationshipModifyBodyDtoValidator : AbstractValidator<OrganizationRelationshipModifyBodyDto>
    {
        public OrganizationRelationshipModifyBodyDtoValidator()
        {
            RuleFor(c => c.Description).MaximumLength(500);
        }
    }
}

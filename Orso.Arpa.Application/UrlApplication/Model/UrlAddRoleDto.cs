using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.ProjectDomain.Commands;

namespace Orso.Arpa.Application.UrlApplication.Model
{
    public class UrlAddRoleDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UrlAddRoleDtoMappingProfile : Profile
    {
        public UrlAddRoleDtoMappingProfile()
        {
            CreateMap<UrlAddRoleDto, AddRoleToUrl.Command>()
                .ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }
    public class UrlAddRoleDtoValidator : AbstractValidator<UrlAddRoleDto>
    {
        public UrlAddRoleDtoValidator()
        {
            RuleFor(d => d)
               .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.RoleId)
                .NotEmpty();
        }
    }
}

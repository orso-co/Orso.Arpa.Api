using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.ProjectDomain.Commands;

namespace Orso.Arpa.Application.UrlApplication.Model
{
    public class UrlRemoveRoleDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UrlRemoveRoleDtoMappingProfile : Profile
    {
        public UrlRemoveRoleDtoMappingProfile()
        {
            CreateMap<UrlRemoveRoleDto, RemoveRoleFromUrl.Command>()
                .ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }

    public class UrlRemoveRoleDtoValidator : AbstractValidator<UrlRemoveRoleDto>
    {
        public UrlRemoveRoleDtoValidator()
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

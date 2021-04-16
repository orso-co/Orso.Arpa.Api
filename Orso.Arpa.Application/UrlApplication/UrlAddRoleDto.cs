using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Persistence.Seed;
using static Orso.Arpa.Domain.Logic.Urls.AddRole;

namespace Orso.Arpa.Application.UrlApplication
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
            CreateMap<UrlAddRoleDto, Command>()
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
                .NotEmpty()
                .NotEqual(RoleSeedData.Admin.Id);
        }
    }
}

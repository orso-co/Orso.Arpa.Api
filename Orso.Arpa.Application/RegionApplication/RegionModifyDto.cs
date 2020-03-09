using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Regions.Modify;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionModifyDto : IModifyDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class RegionModifyDtoMappingProfile : Profile
    {
        public RegionModifyDtoMappingProfile()
        {
            CreateMap<RegionModifyDto, Command>();
        }
    }

    public class RegionModifyDtoValidator : AbstractValidator<RegionModifyDto>
    {
        public RegionModifyDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}

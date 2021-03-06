using System;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Modify
    {
        public class Command : IModifyCommand<Region>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Region>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Region>(arpaContext, localizer);

                RuleFor(c => c.Name)
                    .MustAsync(async (dto, name, cancellation) => !(await arpaContext.Regions
                        .AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != dto.Id, cancellation)))
                    .WithMessage(localizer["A region with the requested name does already exist"]);
            }
        }
    }
}

using System;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Modify
    {
        public class Command : IModifyCommand<Region>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool IsForPerformance { get; set; }
            public bool IsForRehearsal { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Region>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.IsForPerformance, opt => opt.MapFrom(src => src.IsForPerformance))
                    .ForMember(dest => dest.IsForRehearsal, opt => opt.MapFrom(src => src.IsForRehearsal))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Region>(arpaContext);

                RuleFor(c => c.Name)
                    .MustAsync(async (dto, name, cancellation) => !(await arpaContext.Regions
#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
                        .AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != dto.Id, cancellation)))
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
                    .WithMessage("A region with the requested name does already exist");
            }
        }
    }
}

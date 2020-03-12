using System;
using System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
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
            public Validator(IArpaContext arpaContext)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.Regions
                        .AnyAsync(r => r.Id == id, cancellation))
                    .OnFailure(dto => throw new RestException("Region not found", HttpStatusCode.NotFound, new { Id = "Not found" }));

                RuleFor(c => c.Name)
                    .MustAsync(async (dto, name, cancellation) => !(await arpaContext.Regions
                        .AnyAsync(r => r.Name == name && r.Id != dto.Id, cancellation)))
                    .WithMessage("A region with the requested name does already exist");
            }
        }
    }
}

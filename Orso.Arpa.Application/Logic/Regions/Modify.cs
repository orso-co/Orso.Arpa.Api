using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Regions.Modify;

namespace Orso.Arpa.Application.Logic.Regions
{
    public static class Modify
    {
        public class Dto : IModifyDto
        {
            public string Name { get; set; }
            public Guid Id { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>();
            }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Id)
                    .NotEmpty();
                RuleFor(c => c.Name)
                    .NotEmpty();
            }
        }
    }
}

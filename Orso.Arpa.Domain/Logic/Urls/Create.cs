using System;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Create
    {
        public class Command : ICreateCommand<Url>
        {
            public Command(string href, string anchorText, Guid projectId)
            {
                Href = href;
                AnchorText = anchorText;
                ProjectId = projectId;
            }

            public Command()
            {
            }

            public string Href { get; set; }
            public string AnchorText { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Create.Command>()
                    .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Href))
                    .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.AnchorText))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .MustAsync(async (projectId, cancellation) => await arpaContext.Projects.AnyAsync(p => p.Id == projectId, cancellation))
                    .WithMessage("The project could not be found");
            }
        }
    }
}

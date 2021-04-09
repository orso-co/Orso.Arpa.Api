using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class AddUrl
    {
        public class Command : IRequest
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
                CreateMap<Command, AddUrl.Command>()
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
                    .MustAsync(async (ProjectId, cancellation) => await arpaContext.Projects.AnyAsync(p => p.Id == ProjectId, cancellation))
                    .WithMessage("The project could not be found");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(
                IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var newUrl = new Url(Guid.NewGuid(), request);
                await _arpaContext.Urls.AddAsync(newUrl, cancellationToken);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem creating new url");
            }
        }
    }
}

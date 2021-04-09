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
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Project existingProject = await _arpaContext.Projects.FindAsync(new object[] { request.ProjectId }, cancellationToken);

                // create a new Url here and attach it to the existing project
                var command = new Orso.Arpa.Domain.Logic.Urls.Create.Command
                {
                    AnchorText = request.AnchorText,
                    Href = request.Href,
                    ProjectId = existingProject.Id
                };
                var newUrl = new Url(Guid.NewGuid(), command);

                // TODO: shouldn't the above work as something like this to use the auto-mapper?
                //var newUrl = new Url(Guid.NewGuid(), _mapper.Map<Command, Orso.Arpa.Domain.Logic.Urls.Create.Command>(request));

                await _arpaContext.Urls.AddAsync(newUrl, cancellationToken);
                existingProject.Urls.Add(newUrl);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem creating new url");
            }
        }
    }
}

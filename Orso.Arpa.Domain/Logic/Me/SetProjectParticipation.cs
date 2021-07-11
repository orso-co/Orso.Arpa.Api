using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class SetProjectParticipation
    {
        public class Command : IRequest<ProjectParticipation>
        {
            public Guid PersonId { get; set; }
            public Guid ProjectId { get; set; }
            public Guid StatusId { get; set; }
            public Guid MusicianProfileId { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.ProjectId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Project>(arpaContext)
                    .MustAsync(async (projectId, cancellation) => !(await arpaContext.FindAsync<Project>(new object[] { projectId }, cancellation)).IsCompleted)
                    .WithMessage("The project is completed. You may not set the participation of a completed project");

                RuleFor(c => c.StatusId)
                    .SelectValueMapping<Command, ProjectParticipation>(arpaContext, p => p.ParticipationStatusInner);

                RuleFor(c => c.MusicianProfileId)
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(async (command, musicianProfileId, cancellation) => (await arpaContext
                        .EntityExistsAsync<MusicianProfile>(mp => mp.Id == musicianProfileId && mp.PersonId == command.PersonId, cancellation)))
                    .WithErrorCode("404")
                    .WithMessage($"{typeof(MusicianProfile).Name} could not be found.")
                    .MustAsync(async (musicianProfileId, cancellation) => !(await arpaContext.FindAsync<MusicianProfile>(new object[] { musicianProfileId }, cancellation)).IsDeactivated)
                    .WithMessage("Your musician profile is deactivated. A deactivated musician profile may not participate in a project");
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, ProjectParticipation>()
                    .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Comment))
                    .ForMember(dest => dest.ParticipationStatusInnerId, opt => opt.MapFrom(src => src.StatusId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Handler : IRequestHandler<Command, ProjectParticipation>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(IArpaContext arpaContext, IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<ProjectParticipation> Handle(Command request, CancellationToken cancellationToken)
            {
                ProjectParticipation participation = await _arpaContext.ProjectParticipations
                    .SingleOrDefaultAsync(pp => pp.ProjectId == request.ProjectId && pp.MusicianProfileId == request.MusicianProfileId, cancellationToken);

                if (participation == null)
                {
                    participation = new ProjectParticipation(request);
                    participation = (await _arpaContext.ProjectParticipations.AddAsync(participation, cancellationToken)).Entity;
                }
                else
                {
                    _mapper.Map(request, participation);
                    participation = _arpaContext.ProjectParticipations.Update(participation).Entity;
                }

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return participation;
                }

                throw new Exception("Problem setting project participation");
            }
        }
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.MyProjects;

public static class SetProjectParticipationStatus
{
    public class Command : IRequest<ProjectParticipation>
    {
        public string CommentByPerformerInner { get; set; }
        public ProjectParticipationStatusInner ParticipationStatusInner { get; set; }
        public Guid ProjectId { get; set; }
        public Guid MusicianProfileId { get; set; }
    }
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
        {
            _ = RuleFor(c => c.ProjectId)
                .Cascade(CascadeMode.Stop)
                .EntityExists<Command, Project>(arpaContext)
                .MustAsync(async (projectId, cancellation) =>
                    !(await arpaContext.FindAsync<Project>(new object[] { projectId }, cancellation))
                        .IsCompleted)
                .WithMessage(
                    "The project is completed. You may not set the participation of a completed project")
                .MustAsync(async (command, projectId, cancellation) =>
                    await arpaContext.EntityExistsAsync<ProjectParticipation>(
                        pp => pp.ProjectId == projectId &&
                              pp.MusicianProfileId == command.MusicianProfileId, cancellation))
                .WithErrorCode("404")
                .WithMessage("Project Participation could not be found.");

            _ = RuleFor(c => c.MusicianProfileId)
                .Cascade(CascadeMode.Stop)
                .MustAsync(async (musicianProfileId, cancellation) =>
                    await arpaContext.EntityExistsAsync<MusicianProfile>(
                        d => d.Id == musicianProfileId && d.PersonId == tokenAccessor.PersonId,
                        cancellation))
                .WithErrorCode("404")
                .WithMessage("Musician Profile could not be found.")
                .MustAsync(async (musicianProfileId, cancellation) =>
                    !await arpaContext.EntityExistsAsync<MusicianProfileDeactivation>(
                        d => d.MusicianProfileId == musicianProfileId,
                        cancellation))
                .WithMessage("The musician profile is deactivated. A deactivated musician profile may not participate in a project");

        }
    }

    public class Handler : IRequestHandler<Command, ProjectParticipation>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<ProjectParticipation> Handle(Command request, CancellationToken cancellationToken)
        {
            // Das MuPro muss einmal geladen werden, weil sonst die Property MusicianProfile des neuen Objekts null ist
            ProjectParticipation participation = (await _arpaContext.FindAsync<MusicianProfile>(new object[] { request.MusicianProfileId }, cancellationToken))
                .ProjectParticipations.FirstOrDefault(pp => pp.ProjectId == request.ProjectId);

            participation.Update(request);
            participation = _arpaContext.ProjectParticipations.Update(participation).Entity;

            return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                ? participation
                : throw new Exception("Problem setting project participation");
        }
    }
}



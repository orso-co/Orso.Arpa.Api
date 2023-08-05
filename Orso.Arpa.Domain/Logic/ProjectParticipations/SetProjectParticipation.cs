using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class SetProjectParticipation
    {
        public class Command : IRequest<ProjectParticipation>
        {
            public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }
            public ProjectParticipationStatusInternal? ParticipationStatusInternal { get; set; }
            public ProjectInvitationStatus? InvitationStatus { get; set; }
            public string CommentByStaffInner { get; set; }
            public string CommentTeam { get; set; }
            public Guid ProjectId { get; set; }
            public Guid MusicianProfileId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.ProjectId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Project>(arpaContext)
                    .CustomAsync(async (projectId, context, cancellation) =>
                    {
                        Project project = await arpaContext.FindAsync<Project>(new object[] { projectId }, cancellation);
                        if (project.IsCompleted || ProjectStatus.Cancelled.Equals(project.Status))
                        {
                            context.AddFailure(nameof(Command.ProjectId), "The project is cancelled or completed. You must not set the participation of such a project");
                            return;
                        }
                        if (project.Children.Any())
                        {
                            context.AddFailure(nameof(Command.ProjectId), "You may not set the participation of a parent project");
                        }
                        if (await arpaContext.EntityExistsAsync<MusicianProfileDeactivation>(
                         d => d.MusicianProfileId == context.InstanceToValidate.MusicianProfileId && d.DeactivationStart.Date < project.EndDate.GetValueOrDefault().Date,
                        cancellation))
                        {
                            context.AddFailure(nameof(Command.MusicianProfileId), "The musician profile is deactivated during the duration of the project.");
                        }
                    });

                _ = RuleFor(c => c.MusicianProfileId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, MusicianProfile>(arpaContext);
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

                if (participation == null)
                {
                    participation = new ProjectParticipation(request);
                    participation = (await _arpaContext.ProjectParticipations.AddAsync(participation, cancellationToken)).Entity;
                }
                else
                {
                    participation.Update(request);
                    participation = _arpaContext.ProjectParticipations.Update(participation).Entity;
                }

                return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                    ? participation
                    : throw new AffectedRowCountMismatchException(nameof(ProjectParticipation));
            }
        }
    }
}

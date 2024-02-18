using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Commands;

public static class SetMyProjectParticipationStatus
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
                .CustomAsync(async (projectId, context, cancellation) =>
                {
                    Project project = await arpaContext.FindAsync<Project>([projectId], cancellation);
                    if (project.IsCompleted || ProjectStatus.Cancelled.Equals(project.Status))
                    {
                        context.AddFailure("The project is cancelled or completed. You must not set the participation of such a project");
                        return;
                    }
                    if (project.IsHiddenForPerformers)
                    {
                        context.AddFailure("The project is not accessible for your role");
                        return;
                    }
                    if (project.Children.Count > 0)
                    {
                        context.AddFailure("You may not set the participation of a parent project");
                    }
                    if (!ProjectParticipationStatusInner.Refusal.Equals(context.InstanceToValidate.ParticipationStatusInner) && await arpaContext.EntityExistsAsync<MusicianProfileDeactivation>(
                         d => d.MusicianProfileId == context.InstanceToValidate.MusicianProfileId && d.DeactivationStart.Date < project.EndDate.GetValueOrDefault().Date,
                        cancellation))
                    {
                        context.AddFailure(nameof(Command.MusicianProfileId), "The musician profile is deactivated during the duration of the project.");
                    }
                });

            _ = RuleFor(c => c.MusicianProfileId)
                .Cascade(CascadeMode.Stop)
                .MustAsync(async (musicianProfileId, cancellation) =>
                    await arpaContext.EntityExistsAsync<MusicianProfile>(
                        d => d.Id == musicianProfileId && d.PersonId == tokenAccessor.PersonId,
                        cancellation))
                .WithErrorCode("404")
                .WithMessage("Musician Profile could not be found.");
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
                participation = (await _arpaContext.Set<ProjectParticipation>().AddAsync(participation, cancellationToken)).Entity;
            }
            else
            {
                participation.Update(request);
                participation = _arpaContext.Set<ProjectParticipation>().Update(participation).Entity;
            }

            return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                ? participation
                : throw new AffectedRowCountMismatchException(nameof(ProjectParticipation));
        }
    }
}

using System;
using System.Linq;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Misc;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class CreateMusicianProfileDeactivation
    {
        public class Command : ICreateCommand<MusicianProfileDeactivation>
        {
            public Guid MusicianProfileId { get; set; }
            public DateTime DeactivationStart { get; set; }
            public string Purpose { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IDateTimeProvider dateTimeProvider, IArpaContext arpaContext)
            {
                _ = RuleFor(command => command.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                _ = RuleFor(dest => dest.DeactivationStart)
                    .Cascade(CascadeMode.Stop)
                    .Must(start => start >= dateTimeProvider.GetUtcNow())
                    .WithMessage("Deactivating a musician profile for the past is not allowed")
                    .CustomAsync(async (start, context, cancellation) =>
                    {
                        Guid musicianProfileId = context.InstanceToValidate.MusicianProfileId;
                        MusicianProfile musicianProfile = await arpaContext.FindAsync<MusicianProfile>(new object[] { musicianProfileId }, cancellation);
                        if (musicianProfile.Deactivation != null)
                        {
                            context.AddFailure(nameof(Command.MusicianProfileId), "The musician profile is already deactivated");
                            return;
                        }
                        if (musicianProfile.ProjectParticipations
                        .Where(pp => ProjectParticipationStatusInner.Acceptance.Equals(pp.ParticipationStatusInner))
                        .Select(pp => pp.Project).Any(project =>
                                project.EndDate.GetValueOrDefault().Date >= start.Date
                                && !project.IsCompleted
                                && project.Status != ProjectStatus.Cancelled
                                && !project.IsHiddenForPerformers))
                        {
                            context.AddFailure("You may not deactivate a musician profile which is participating in an active project");
                        }
                    });
            }
        }
    }
}

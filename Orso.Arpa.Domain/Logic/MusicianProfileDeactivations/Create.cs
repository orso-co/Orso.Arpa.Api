using System;
using System.Linq;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.MusicianProfileDeactivations
{
    public static class Create
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
                RuleFor(command => command.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                RuleFor(dest => dest.DeactivationStart)
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
                        if (musicianProfile.ProjectParticipations.Select(pp => pp.Project).Any(project => project.EndDate >= start && !project.IsCompleted))
                        {
                            context.AddFailure("You may not deactivate a musician profile which is participating in an active project");
                        }
                    });
            }
        }
    }
}

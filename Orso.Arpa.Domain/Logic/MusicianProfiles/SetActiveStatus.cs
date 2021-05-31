using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class SetActiveStatus
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public bool Active { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
            {
                CascadeMode = CascadeMode.Stop;

                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicianProfile>(arpaContext, nameof(Command.Id));

                if (!tokenAccessor.UserRoles.Contains(RoleNames.Staff))
                {
                    RuleFor(c => c.Id)
                        .MustAsync(async (musicianProfileId, cancellation) => await arpaContext
                            .EntityExistsAsync<MusicianProfile>(mp => mp.Id == musicianProfileId && mp.PersonId == tokenAccessor.PersonId, cancellation))
                        .OnFailure(_ => throw new AuthorizationException("This musician profile is not yours. You don't have access to this musician profile."));
                }

                RuleFor(c => c.Active)
                    .MustAsync(async (command, active, cancellation) => active ? true : !(await arpaContext.FindAsync<MusicianProfile>(new object[] { command.Id }, cancellation)).ProjectParticipations.Select(pp => pp.Project).All(p => p.IsCompleted))
                    .WithMessage("You may not deactivate a musician profile which is participating in an active project");
                // ToDo: Add additional validation rules if appropriate
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MusicianProfile profile = await _arpaContext.FindAsync<MusicianProfile>(new object[] { request.Id }, cancellationToken);
                profile.SetActiveStatus(request.Active);

                _arpaContext.MusicianProfiles.Update(profile);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating Musician Profile");
            }
        }
    }
}

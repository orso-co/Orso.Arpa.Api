using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class ModifyDoublingInstrument
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid MusicianProfileId { get; set; }
            public byte LevelAssessmentInner { get; set; }
            public Guid? AvailabilityId { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<MusicianProfileSection>(s => s.Id == id && s.MusicianProfileId == command.MusicianProfileId, cancellation))
                    .WithMessage("Doubling instrument with this id and the supplied musician profile does not exist");

                RuleFor(c => c.AvailabilityId)
                    .SelectValueMapping<Command, MusicianProfileSection>(arpaContext, a => a.InstrumentAvailability);
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
                MusicianProfileSection existingEntity = await _arpaContext.GetByIdAsync<MusicianProfileSection>(request.Id, cancellationToken);

                existingEntity.Update(request);

                _arpaContext.Entry(existingEntity)?.CurrentValues?.SetValues(existingEntity);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating doubling instrument");
            }
        }
    }
}

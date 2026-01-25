using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class AddPrioritizedPiece
    {
        public class Command : IRequest
        {
            public Guid AppointmentId { get; set; }
            public Guid SetlistPieceId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.AppointmentId)
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(c => c.SetlistPieceId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, SetlistPiece>(arpaContext)
                    .MustAsync(async (dto, setlistPieceId, cancellation) =>
                        !await arpaContext.AppointmentSetlistPieces.AnyAsync(
                            asp => asp.AppointmentId == dto.AppointmentId && asp.SetlistPieceId == setlistPieceId,
                            cancellation))
                    .WithMessage("This piece is already prioritized for this appointment");
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
                var appointmentSetlistPiece = new AppointmentSetlistPiece(
                    null,
                    request.AppointmentId,
                    request.SetlistPieceId);

                await _arpaContext.AppointmentSetlistPieces.AddAsync(appointmentSetlistPiece, cancellationToken);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(AppointmentSetlistPiece));
            }
        }
    }
}

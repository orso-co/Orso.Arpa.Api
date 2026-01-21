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
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class SetAppointmentParticipationCommentByStaff
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public string CommentByStaffInner { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);
                _ = RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
                _ = RuleFor(d => d.CommentByStaffInner)
                    .MaximumLength(500);
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
                AppointmentParticipation appointmentParticipation = await _arpaContext.AppointmentParticipations
                    .SingleOrDefaultAsync(ap => ap.AppointmentId.Equals(request.Id) && ap.PersonId.Equals(request.PersonId), cancellationToken);

                if (appointmentParticipation == null)
                {
                    throw new NotFoundException(nameof(AppointmentParticipation), $"AppointmentId: {request.Id}, PersonId: {request.PersonId}");
                }

                appointmentParticipation.SetCommentByStaffInner(request.CommentByStaffInner);
                _ = _arpaContext.AppointmentParticipations.Update(appointmentParticipation);

                return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                    ? Unit.Value
                    : throw new AffectedRowCountMismatchException(nameof(AppointmentParticipation));
            }
        }
    }
}

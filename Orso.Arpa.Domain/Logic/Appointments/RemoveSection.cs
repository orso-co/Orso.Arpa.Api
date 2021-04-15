using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class RemoveSection
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid sectionId)
            {
                Id = id;
                SectionId = sectionId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid SectionId { get; private set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(d => d.SectionId)
                    .MustAsync(async (dto, sectionId, cancellation) => await arpaContext.SectionAppointments
                        .AnyAsync(sa => sa.SectionId == sectionId && sa.AppointmentId == dto.Id, cancellation))
                    .WithMessage(localizer["The section is not linked to the appointment"]);
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
                SectionAppointment sectionAppointment = await _arpaContext.SectionAppointments
                    .FirstOrDefaultAsync(sa => sa.SectionId == request.SectionId && sa.AppointmentId == request.Id, cancellationToken);

                _arpaContext.SectionAppointments.Remove(sectionAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new Exception("Problem removing section appointment");
            }
        }
    }
}

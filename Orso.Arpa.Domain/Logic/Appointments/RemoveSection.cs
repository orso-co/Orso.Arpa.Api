using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;

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
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(request.Id);

                SectionAppointment sectionToRemove = existingAppointment.SectionAppointments.FirstOrDefault(r => r.SectionId == request.SectionId);

                existingAppointment.SectionAppointments.Remove(sectionToRemove);

                _arpaContext.Appointments.Update(existingAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}

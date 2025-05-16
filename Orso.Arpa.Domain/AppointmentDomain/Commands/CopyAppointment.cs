using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class CopyAppointment
    {
        public class Command : IRequest<Appointment>
        {
            public Guid AppointmentIdToCopy { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.AppointmentIdToCopy)
                    .EntityExists<Command, Appointment>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, Appointment>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Appointment> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment appointmentToCopy = await _context.Appointments
                    .FirstOrDefaultAsync(a => a.Id == request.AppointmentIdToCopy, cancellationToken);

                var newAppointment = new Appointment(Guid.NewGuid(), request, appointmentToCopy);

                appointmentToCopy.SectionAppointments
                    .ToList()
                    .ForEach(sectionAppointment =>
                    {
                        var newSectionAppointment = new SectionAppointment(null, sectionAppointment.Section, newAppointment);
                        newAppointment.SectionAppointments.Add(newSectionAppointment);
                    });

                appointmentToCopy.ProjectAppointments
                    .ToList()
                    .ForEach(projectAppointment =>
                    {
                        var newProjectAppointment = new ProjectAppointment(null, projectAppointment.Project, newAppointment);
                        newAppointment.ProjectAppointments.Add(newProjectAppointment);
                    });

                appointmentToCopy.AppointmentRooms
                    .ToList()
                    .ForEach(appointmentRoom =>
                    {
                        var newAppointmentRoom = new AppointmentRoom(null, newAppointment, appointmentRoom.Room);
                        newAppointment.AppointmentRooms.Add(newAppointmentRoom);
                    });

                EntityEntry<Appointment> result = await _context.Appointments.AddAsync(newAppointment, cancellationToken);

                if (await _context.SaveChangesAsync(cancellationToken) > 0)
                {
                    // if we return createResult.Entity directly, some navigation properties may not be loaded properly
                    _context.ClearChangeTracker();
                    return await _context.Set<Appointment>().FindAsync([result.Entity.Id], cancellationToken);
                }

                throw new AffectedRowCountMismatchException(typeof(Appointment).Name);
            }
        }
    }
}

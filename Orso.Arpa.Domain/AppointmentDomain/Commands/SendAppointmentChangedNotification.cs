using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class SendAppointmentChangedNotification
    {
        public class Command : IRequest
        {
            public Guid AppointmentId { get; set; }
            public bool ForceSending { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(x => x.AppointmentId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Appointment>(arpaContext)
                    .CustomAsync(async (appointmentId, context, cancellation) =>
                    {
                        Appointment appointment = await arpaContext.FindAsync<Appointment>([appointmentId], cancellation);
                        var appointmentSectionsCount = appointment.SectionAppointments.Count;
                        var appointmentProjectCount = appointment.ProjectAppointments.Count;
                        if (appointmentSectionsCount == 0 && appointmentProjectCount == 0)
                        {
                            context.AddFailure("The appointment has no sections or projects. Please add at least one section or project to the appointment to prevent sending too many e-mails.");
                            return;
                        }
                    });

                RuleFor(x => x.ForceSending)
                    .CustomAsync(async (appointmentId, context, cancellation) =>
                    {
                        var doesAppointmentProjectExist = await arpaContext
                            .EntityExistsAsync<ProjectAppointment>(pa => pa.AppointmentId == context.InstanceToValidate.AppointmentId, cancellation);
                        var doesAppointmentSectionExist = await arpaContext
                            .EntityExistsAsync<SectionAppointment>(pa => pa.AppointmentId == context.InstanceToValidate.AppointmentId, cancellation);

                        if (doesAppointmentSectionExist && !doesAppointmentProjectExist && !context.InstanceToValidate.ForceSending)
                        {
                            context.AddFailure("The appointment has no projects. Are you sure you want to do this?");
                        }
                    });
            }
        }

        public class Handler(
            JwtConfiguration jwtConfiguration,
            ClubConfiguration clubConfiguration,
            IEmailSender emailSender,
            IArpaContext arpaContext) : IRequestHandler<Command>
        {
            private readonly JwtConfiguration _jwtConfiguration = jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration = clubConfiguration;
            private readonly IEmailSender _emailSender = emailSender;
            private readonly IArpaContext _arpaContext = arpaContext;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                List<Guid> personIds = await _arpaContext
                    .GetPersonsForAppointment(request.AppointmentId)
                    .Select(a => a.Id)
                    .ToListAsync(cancellationToken);

                if (personIds.Count == 0)
                {
                    throw new ValidationException([new ValidationFailure(nameof(request.AppointmentId), "No persons are eligible for this appointment. Cannot send email to empty recipient list.")]);
                }

                List<Person> persons = await _arpaContext.Persons
                    .AsQueryable()
                    .Where(p => personIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                Appointment appointment = await _arpaContext.FindAsync<Appointment>([request.AppointmentId], cancellationToken);

                var template = new AppointmentChangedByStaffTemplate
                {
                    ArpaLogo = _jwtConfiguration.ArpaLogo,
                    ClubAddress = _clubConfiguration.Address,
                    ClubMail = _clubConfiguration.ContactEmail,
                    ClubName = _clubConfiguration.Name,
                    ClubPhoneNumber = _clubConfiguration.Phone,
                    AppointmentName = appointment.ToString(),
                    DateAndTime = $"{appointment.StartTime.ToGermanDateTimeString()} - {appointment.EndTime.ToGermanTimeString()}",
                    PublicDetails = appointment.PublicDetails,
                    Venue = appointment.Venue?.ToString(),
                    ArpaUrl = _jwtConfiguration.Audience,
                    Status = appointment.Status.ToString(),
                    Sections = string.Join(", ", appointment.SectionAppointments.Select(sa => sa.Section.ToString()))
                };

                await _emailSender.SendTemplatedEmailAsync(template, persons
                    .Select(person => person.GetPreferredEMailAddress())
                    .Where(email => !string.IsNullOrWhiteSpace(email)));

                return Unit.Value;
            }
        }
    }
}

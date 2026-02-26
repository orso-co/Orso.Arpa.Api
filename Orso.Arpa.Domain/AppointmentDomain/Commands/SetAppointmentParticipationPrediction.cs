using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Notifications;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class SetAppointmentParticipationPrediction
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public AppointmentParticipationPrediction Prediction { get; set; }
            public string CommentByPerformerInner { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<Command, CreateAppointmentParticipation.Command>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.Prediction, opt => opt.MapFrom(src => src.Prediction))
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(_ => default(AppointmentParticipationResult?)))
                    .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.CommentByPerformerInner));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext)
                    .MustAsync(async (id, cancellation) =>
                    {
                        var appointment = await arpaContext.Appointments.FirstOrDefaultAsync(a => a.Id == id, cancellation);
                        return appointment?.Type != AppointmentType.InfoOnly;
                    })
                    .WithMessage("Cannot set participation prediction on an info-only appointment");
                _ = RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper,
                IMediator mediator)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                AppointmentParticipation appointmentParticipation = await _arpaContext.AppointmentParticipations
                    .SingleOrDefaultAsync(ap => ap.AppointmentId.Equals(request.Id) && ap.PersonId.Equals(request.PersonId), cancellationToken);

                if (appointmentParticipation == null)
                {
                    _ = await _arpaContext.AppointmentParticipations
                        .AddAsync(new AppointmentParticipation(Guid.NewGuid(), _mapper.Map<Command, CreateAppointmentParticipation.Command>(request)), cancellationToken);
                }
                else
                {
                    appointmentParticipation.Update(request);
                    _ = _arpaContext.AppointmentParticipations.Update(appointmentParticipation);
                }

                if (await _arpaContext.SaveChangesAsync(cancellationToken) != 2)
                {
                    throw new AffectedRowCountMismatchException(nameof(AppointmentParticipation));
                }

                // Send push notification about participation change
                var appointment = await _arpaContext.Appointments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
                var person = await _arpaContext.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == request.PersonId, cancellationToken);

                if (appointment != null && person != null)
                {
                    _ = _mediator.Publish(new AppointmentParticipationChangedNotification
                    {
                        AppointmentId = request.Id,
                        AppointmentName = appointment.Name ?? appointment.ToString(),
                        PersonId = request.PersonId,
                        PersonName = $"{person.GivenName} {person.Surname}".Trim(),
                        Prediction = request.Prediction.ToString(),
                        ChangedByStaff = false
                    }, cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}

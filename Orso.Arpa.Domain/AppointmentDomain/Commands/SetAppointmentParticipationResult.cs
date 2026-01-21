using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class SetAppointmentParticipationResult
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public AppointmentParticipationResult Result { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<Command, CreateAppointmentParticipation.Command>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.Prediction, opt => opt.MapFrom(_ => default(AppointmentParticipationPrediction?)))
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Appointment>(arpaContext)
                    .Must(appointmentId =>
                    {
                        var appointment = arpaContext.Appointments.Find(appointmentId);
                        if (appointment == null) return true;
                        return appointment.StartTime.Date <= DateTime.Today;
                    })
                    .WithErrorCode("422")
                    .WithMessage("Result can only be set on or after the appointment date.");
                _ = RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                AppointmentParticipation participation = existingAppointment.AppointmentParticipations
                    .FirstOrDefault(pa => pa.PersonId == request.PersonId);

                if (participation == null)
                {
                    participation = new AppointmentParticipation(Guid.NewGuid(), _mapper.Map<Command, CreateAppointmentParticipation.Command>(request));
                    _ = await _arpaContext.AppointmentParticipations.AddAsync(participation, cancellationToken);
                }
                else
                {
                    participation.Update(request);
                    _ = _arpaContext.AppointmentParticipations.Update(participation);
                }

                return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                    ? Unit.Value
                    : throw new AffectedRowCountMismatchException(participation.GetType().Name);
            }
        }
    }
}

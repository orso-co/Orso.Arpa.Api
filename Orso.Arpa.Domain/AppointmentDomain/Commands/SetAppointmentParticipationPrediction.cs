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
                    .EntityExists<Command, Appointment>(arpaContext);
                _ = RuleFor(d => d.PersonId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Person>(arpaContext)
                    .Must((command, personId) => arpaContext.IsPersonEligibleForAppointment(personId, command.Id))
                    .WithErrorCode("403")
                    .WithMessage("This person is not eligible for the supplied appointment.");
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

                return await _arpaContext.SaveChangesAsync(cancellationToken) == 2 // participation + audtit trail
                    ? Unit.Value
                    : throw new AffectedRowCountMismatchException(nameof(AppointmentParticipation));
            }
        }
    }
}

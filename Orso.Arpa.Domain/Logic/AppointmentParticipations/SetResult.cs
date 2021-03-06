using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class SetResult
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid ResultId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, AppointmentParticipation>()
                    .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.ResultId))
                    .ForAllOtherMembers(opt => opt.Ignore());

                CreateMap<Command, Create.Command>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.PredictionId, opt => opt.MapFrom(src => default(Guid?)))
                    .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.ResultId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext, nameof(Command.Id));
                RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext, nameof(Command.PersonId));
                RuleFor(d => d.ResultId)
                    .EntityExists<Command, SelectValueMapping>(arpaContext, nameof(Command.ResultId));
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
                    participation = new AppointmentParticipation(Guid.NewGuid(), _mapper.Map<Command, Create.Command>(request));
                    await _arpaContext.AppointmentParticipations.AddAsync(participation, cancellationToken);
                }
                else
                {
                    _mapper.Map(request, participation);
                    _arpaContext.AppointmentParticipations.Update(participation);
                }

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment participation");
            }
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class SetDates
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>()
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom((src, dest) => src.StartTime ?? dest.StartTime))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom((src, dest) => src.EndTime ?? dest.EndTime))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, IMapper mapper, IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext, localizer);
                RuleFor(d => d.EndTime)
                    .MustAsync(async (request, endTime, cancellation) =>
                    {
                        Appointment existingAppointment = await arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellation);
                        mapper.Map(request, existingAppointment);
                        return existingAppointment?.EndTime >= existingAppointment?.StartTime;
                    })
                    .WithMessage(localizer["EndTime must be later than StartTime"]);
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
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);

                _mapper.Map(request, existingAppointment);

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

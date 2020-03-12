using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class SetVenue
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid? venueId)
            {
                Id = id;
                VenueId = venueId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid? VenueId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>()
                    .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.VenueId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.Appointments
                        .AnyAsync(a => a.Id == id, cancellation))
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));

                RuleFor(d => d.VenueId)
                    .MustAsync(async (venueId, cancellation) => venueId == null || await arpaContext.Venues
                        .AnyAsync(a => a.Id == venueId, cancellation))
                    .OnFailure(dto => throw new RestException("Venue not found", HttpStatusCode.NotFound, new { Venue = "Not found" }));
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
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(request.Id);

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

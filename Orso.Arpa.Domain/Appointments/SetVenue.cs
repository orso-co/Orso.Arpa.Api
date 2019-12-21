using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
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

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _repository.GetByIdAsync<Appointment>(request.Id);

                if (existingAppointment == null)
                {
                    throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" });
                }

                if (request.VenueId.HasValue)
                {
                    Venue venue = await _repository.GetByIdAsync<Venue>(request.VenueId.Value);
                    if (venue == null)
                    {
                        throw new RestException("Venue not found", HttpStatusCode.NotFound, new { Venue = "Not found" });
                    }
                }

                _mapper.Map<Command, Appointment>(request, existingAppointment);

                _repository.Update(existingAppointment);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}

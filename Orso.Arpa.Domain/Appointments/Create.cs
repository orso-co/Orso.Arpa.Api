using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class Create
    {
        public class Command : IRequest<Appointment>
        {
            public Guid? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Name { get; set; }
            public string PublicDetails { get; set; }
            public string InternalDetails { get; set; }
            public Guid? StatusId { get; set; }
            public Guid? EmolumentId { get; set; }
            public Guid? EmolumentPatternId { get; set; }
            public Guid? VenueId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>();
            }
        }

        public class Handler : IRequestHandler<Command, Appointment>
        {
            private readonly IRepository _repository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Appointment> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment region = _mapper.Map<Appointment>(request);

                Appointment createdAppointment = await _repository.AddAsync(region);

                if (await _unitOfWork.CommitAsync())
                {
                    return createdAppointment;
                }

                throw new Exception("Problem creating appointment");
            }
        }
    }
}

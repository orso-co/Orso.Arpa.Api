using System;
using System.Linq;
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
    public static class AddRegister
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid registerId)
            {
                Id = id;
                RegisterId = registerId;
            }

            public Guid Id { get; private set; }
            public Guid RegisterId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, RegisterAppointment>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RegisterId, opt => opt.MapFrom(src => src.RegisterId));
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

                RegisterAppointment existingRegister = existingAppointment.RegisterAppointments.FirstOrDefault(r => r.RegisterId == request.RegisterId);

                if (existingRegister != null)
                {
                    throw new RestException("The register is already linked to the appointment", HttpStatusCode.BadRequest, new { Register = "Already linked" });
                }

                existingAppointment.RegisterAppointments.Add(_mapper.Map<RegisterAppointment>(request));

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

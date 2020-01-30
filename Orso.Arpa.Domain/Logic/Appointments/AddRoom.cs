using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class AddRoom
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid roomId)
            {
                Id = id;
                RoomId = roomId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid RoomId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, AppointmentRoom>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository readOnlyRepository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
                RuleFor(d => d.RoomId)
                    .MustAsync(async (roomId, cancellation) => await readOnlyRepository.GetByIdAsync<Room>(roomId) != null)
                    .OnFailure(dto => throw new RestException("Room not found", HttpStatusCode.NotFound, new { Room = "Not found" }))
                    .MustAsync(async (dto, roomId, cancellation) => !(await readOnlyRepository
                        .GetByIdAsync<Appointment>(dto.Id)).AppointmentRooms
                            .Any(ar => ar.RoomId == roomId))
                    .WithMessage("The room is already linked to the appointment");
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

                existingAppointment.AppointmentRooms.Add(_mapper.Map<AppointmentRoom>(request));

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

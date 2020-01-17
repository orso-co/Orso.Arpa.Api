using System.Linq;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class AddRoomDtoValidator : AbstractValidator<AddRoomDto>
    {
        public AddRoomDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.RoomId)
                .NotEmpty()
                .MustAsync(async (roomId, cancellation) => await readOnlyRepository.GetByIdAsync<Room>(roomId) != null)
                .OnFailure(dto => throw new RestException("Room not found", HttpStatusCode.NotFound, new { Room = "Not found" }))
                .MustAsync(async (dto, roomId, cancellation) => !(await readOnlyRepository
                    .GetByIdAsync<Appointment>(dto.Id)).AppointmentRooms
                        .Any(ar => ar.RoomId == roomId))
                .WithMessage("The room is already linked to the appointment");
        }
    }
}

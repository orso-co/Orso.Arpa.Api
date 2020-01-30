using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class RemoveRoomDtoValidator : AbstractValidator<RemoveRoomDto>
    {
        public RemoveRoomDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.RoomId)
                .NotEmpty();
        }
    }
}

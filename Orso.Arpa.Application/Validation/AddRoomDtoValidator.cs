using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class AddRoomDtoValidator : AbstractValidator<AddRoomDto>
    {
        public AddRoomDtoValidator()
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

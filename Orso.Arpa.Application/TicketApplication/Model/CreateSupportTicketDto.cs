using System.Collections.Generic;
using FluentValidation;

namespace Orso.Arpa.Application.TicketApplication.Model
{
    public class CreateSupportTicketDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public List<string> Topics { get; set; } = new();
    }

    public class CreateSupportTicketDtoValidator : AbstractValidator<CreateSupportTicketDto>
    {
        public CreateSupportTicketDtoValidator()
        {
            RuleFor(c => c.GivenName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(c => c.Message)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(5000);

            RuleFor(c => c.Topics)
                .NotEmpty()
                .WithMessage("At least one topic must be selected.");
        }
    }
}

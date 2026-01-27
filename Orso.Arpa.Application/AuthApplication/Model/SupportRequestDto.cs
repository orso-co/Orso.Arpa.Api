using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class SupportRequestDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public List<string> Topics { get; set; } = new();
    }

    public class SupportRequestDtoMappingProfile : Profile
    {
        public SupportRequestDtoMappingProfile()
        {
            CreateMap<SupportRequestDto, SendSupportRequest.Command>();
        }
    }

    public class SupportRequestDtoValidator : AbstractValidator<SupportRequestDto>
    {
        public SupportRequestDtoValidator()
        {
            RuleFor(q => q.GivenName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(q => q.Surname)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(q => q.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(q => q.Username)
                .MaximumLength(256)
                .When(q => !string.IsNullOrEmpty(q.Username));

            RuleFor(q => q.Message)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(5000);

            RuleFor(q => q.Topics)
                .NotEmpty()
                .WithMessage("At least one topic must be selected.");
        }
    }
}

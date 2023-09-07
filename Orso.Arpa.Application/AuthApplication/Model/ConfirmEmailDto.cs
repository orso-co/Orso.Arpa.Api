using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ConfirmEmailDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }

    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(c => c.Token)
                .NotEmpty();
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class ConfirmEmailDtoMappingProfile : Profile
    {
        public ConfirmEmailDtoMappingProfile()
        {
            CreateMap<ConfirmEmailDto, ConfirmEmail.Command>()
                .ForMember(cmd => cmd.Token, opt => opt.MapFrom(dto => Uri.UnescapeDataString(dto.Token)));
        }
    }
}

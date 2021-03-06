using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.AuthApplication
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string ClientUri { get; set; }
    }

    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator(IStringLocalizer<ApplicationResource> localizer)
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .Username(localizer);
            RuleFor(c => c.Password)
                .Password(localizer);
            RuleFor(c => c.Email)
                .NotEmpty()
                .MaximumLength(256)
                .EmailAddress();
            RuleFor(c => c.GivenName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Surname)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.ClientUri)
                .ValidUri();
        }
    }

    public class UserRegisterDtoMappingProfile : Profile
    {
        public UserRegisterDtoMappingProfile()
        {
            CreateMap<UserRegisterDto, UserRegister.Command>();
            CreateMap<UserRegisterDto, CreateEmailConfirmationToken.Command>()
                .ForMember(cmd => cmd.UsernameOrEmail, opt => opt.MapFrom(dto => dto.Email));
        }
    }
}

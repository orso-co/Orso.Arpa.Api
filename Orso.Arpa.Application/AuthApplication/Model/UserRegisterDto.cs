using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }

        public Guid GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ClientUri { get; set; }
        public IList<Guid> StakeholderGroupIds { get; set; } = new List<Guid>();
    }

    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .Username();
            RuleFor(c => c.Password)
                .Password();
            RuleFor(c => c.Email)
                .NotEmpty()
                .MaximumLength(256)
                .EmailAddress();
            RuleFor(c => c.GivenName)
                .NotEmpty()
                .PersonName(50);
            RuleFor(c => c.Surname)
                .NotEmpty()
                .PersonName(50);
            RuleFor(c => c.ClientUri)
                .ValidUri();
            RuleFor(c => c.StakeholderGroupIds)
                .NotNull();
            RuleFor(c => c.GenderId)
                .NotEmpty();
            RuleFor(c => c.AboutMe)
                .RestrictedFreeText(1000);
        }
    }

    public class UserRegisterDtoMappingProfile : Profile
    {
        public UserRegisterDtoMappingProfile()
        {
            CreateMap<UserRegisterDto, RegisterUser.Command>();
            CreateMap<UserRegisterDto, CreateEmailConfirmationToken.Command>()
                .ForMember(cmd => cmd.UsernameOrEmail, opt => opt.MapFrom(dto => dto.Email));
        }
    }
}

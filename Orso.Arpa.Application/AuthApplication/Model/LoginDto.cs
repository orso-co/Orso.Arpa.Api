using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoMappingProfile : Profile
    {
        public LoginDtoMappingProfile()
        {
            CreateMap<LoginDto, Command>();
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(q => q.UsernameOrEmail)
                .NotEmpty();
            RuleFor(q => q.Password)
                .NotEmpty();
            When(dto => dto.UsernameOrEmail != null && dto.UsernameOrEmail.Contains('@'), () =>
            {
                RuleFor(dto => dto.UsernameOrEmail).EmailAddress().MaximumLength(256);
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.UsernameOrEmail).Username();
            });
        }
    }
}

using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.ResetPassword;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ResetPasswordDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty();
            RuleFor(c => c.Password)
                .Password();
            RuleFor(c => c.Token)
                .NotEmpty();
        }
    }

    public class ResetPasswordDtoMappingProfile : Profile
    {
        public ResetPasswordDtoMappingProfile()
        {
            CreateMap<ResetPasswordDto, Command>();
        }
    }
}

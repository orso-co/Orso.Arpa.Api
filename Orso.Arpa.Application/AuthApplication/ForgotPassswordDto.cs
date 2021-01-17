using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ForgotPasswordDto
    {
        public string UserName { get; set; }
    }

    public class ForgotPasswordDtoMappingProfile : Profile
    {
        public ForgotPasswordDtoMappingProfile()
        {
            CreateMap<ForgotPasswordDto, ForgotPassword.Command>();
        }
    }

    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(q => q.UserName)
                .NotEmpty();
        }
    }
}

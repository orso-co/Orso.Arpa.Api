using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordDtoMappingProfile : Profile
    {
        public ChangePasswordDtoMappingProfile()
        {
            CreateMap<ChangePasswordDto, Command>();
        }
    }

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(c => c.NewPassword)
                .Password();
            RuleFor(c => c.CurrentPassword)
                .NotEmpty();
        }
    }
}
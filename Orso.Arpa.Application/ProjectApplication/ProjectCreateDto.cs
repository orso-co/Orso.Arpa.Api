using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Projects.Create;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectCreateDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class ProjectCreateDtoMappingProfile : Profile
    {
        public ProjectCreateDtoMappingProfile()
        {
            CreateMap<ProjectCreateDto, Command>();
        }
    }

    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .MaximumLength(1000);
        }
    }
}

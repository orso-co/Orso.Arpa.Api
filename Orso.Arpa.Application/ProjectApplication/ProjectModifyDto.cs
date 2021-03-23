using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Projects.Modify;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // TODO: define further properties according to https://orso.atlassian.net/browse/ARPA-207
    }

    public class ProjectModifyDtoMappingProfile : Profile
    {
        public ProjectModifyDtoMappingProfile()
        {
            CreateMap<ProjectModifyDto, Command>();
        }
    }


    public class ProjectModifyDtoValidator : AbstractValidator<ProjectModifyDto>
    {
        public ProjectModifyDtoValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty();

            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .MaximumLength(1000);
        }
    }
}

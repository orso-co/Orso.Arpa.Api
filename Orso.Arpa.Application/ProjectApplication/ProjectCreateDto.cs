using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Projects.Create;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectCreateDto
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? GenreId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? StateId { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
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
                .FreeText(100);

            RuleFor(p => p.ShortTitle)
                .NotEmpty()
                .PlaceName(30);

            RuleFor(p => p.Description)
                .RestrictedFreeText(1000);

            RuleFor(p => p.Code)
                .NotEmpty()
                .Sepa()
                .MaximumLength(15);

            When(p => p.StartDate != null && p.EndDate != null, () =>
            {
                RuleFor(p => p.EndDate)
                    .Must((p, endTime) => endTime >= p.StartDate)
                    .WithMessage("'EndDate' must be greater than 'StartDate'");
            });
        }
    }
}

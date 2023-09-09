using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.ProjectApplication.Model
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
        public ProjectStatus? Status { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsHiddenForPerformers { get; set; }
    }

    public class ProjectCreateDtoMappingProfile : Profile
    {
        public ProjectCreateDtoMappingProfile()
        {
            _ = CreateMap<ProjectCreateDto, CreateProject.Command>();
        }
    }

    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            _ = RuleFor(p => p.Title)
                .NotEmpty()
                .FreeText(100);

            _ = RuleFor(p => p.ShortTitle)
                .NotEmpty()
                .PlaceName(30);

            _ = RuleFor(p => p.Description)
                .RestrictedFreeText(1000);

            _ = RuleFor(p => p.Code)
                .NotEmpty()
                .Sepa()
                .MaximumLength(15);

            _ = RuleFor(p => p.Status)
                .IsInEnum();

            _ = When(p => p.StartDate != null && p.EndDate != null, () =>
            {
                _ = RuleFor(p => p.EndDate)
                    .Must((p, endTime) => endTime >= p.StartDate)
                    .WithMessage("'EndDate' must be greater than 'StartDate'");
            });
        }
    }
}

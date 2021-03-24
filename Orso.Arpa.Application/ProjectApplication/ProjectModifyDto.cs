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
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? GenreId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // TODO public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();
        public Guid? StateId { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
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
                .MaximumLength(100);

            RuleFor(p => p.ShortTitle)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.Description)
                .MaximumLength(1000);

            RuleFor(p => p.Number)
                .NotEmpty()
                .MaximumLength(15);

            When(p => p.StartDate != null && p.EndDate != null, () =>
            {
                RuleFor(p => p.EndDate)
                .Must((p, endTime) => endTime >= p.StartDate)
                .WithMessage("EndDate must be greater than StartTime");
            });
        }
    }
}

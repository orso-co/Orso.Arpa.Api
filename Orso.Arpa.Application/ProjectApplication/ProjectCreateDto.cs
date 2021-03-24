using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Projects.Create;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectCreateDto
    {
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

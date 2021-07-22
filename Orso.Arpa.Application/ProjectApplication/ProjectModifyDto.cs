using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Projects.Modify;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectModifyDto : IdFromRouteDto<ProjectModifyBodyDto>
    {
    }

    public class ProjectModifyBodyDto
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

    public class ProjectModifyDtoMappingProfile : Profile
    {
        public ProjectModifyDtoMappingProfile()
        {
            CreateMap<ProjectModifyDto, Command>()
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Body.IsCompleted))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Body.ParentId))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.Body.StateId))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Body.EndDate))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Body.StartDate))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Body.GenreId))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Body.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.ShortTitle, opt => opt.MapFrom(src => src.Body.ShortTitle))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Body.Title));
        }
    }

    public class ProjectModifyDtoValidator : IdFromRouteDtoValidator<ProjectModifyDto, ProjectModifyBodyDto>
    {
        public ProjectModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new ProjectModifyBodyDtoValidator());

            RuleFor(p => p.Body.ParentId)
                .Must((dto, parentId) => dto.Id != parentId.Value)
                .When(dto => dto.Body?.ParentId != null)
                .WithMessage("The project must not be its own parent");
        }
    }

    public class ProjectModifyBodyDtoValidator : AbstractValidator<ProjectModifyBodyDto>
    {
        public ProjectModifyBodyDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .GeneralText(100);

            RuleFor(p => p.ShortTitle)
                .NotEmpty()
                .GeneralText(30);

            RuleFor(p => p.Description)
                .GeneralText(1000);

            RuleFor(p => p.Code)
                .NotEmpty()
                .Sepa()
                .MaximumLength(15);

            When(p => p.StartDate != null && p.EndDate != null, () =>
            {
                RuleFor(p => p.EndDate)
                .Must((p, endTime) => endTime >= p.StartDate)
                .WithMessage("EndDate must be greater than EndDate");
            });
        }
    }
}

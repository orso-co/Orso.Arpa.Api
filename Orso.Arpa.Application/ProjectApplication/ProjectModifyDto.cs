using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
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
        public ProjectStatus? Status { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsHiddenForPerformers { get; set; }
    }

    public class ProjectModifyDtoMappingProfile : Profile
    {
        public ProjectModifyDtoMappingProfile()
        {
            _ = CreateMap<ProjectModifyDto, Command>()
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Body.IsCompleted))
                .ForMember(dest => dest.IsHiddenForPerformers, opt => opt.MapFrom(src => src.Body.IsHiddenForPerformers))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Body.ParentId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Body.Status))
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
            _ = RuleFor(d => d.Body)
                .SetValidator(new ProjectModifyBodyDtoValidator());

            _ = RuleFor(p => p.Body.ParentId)
                .Must((dto, parentId) => dto.Id != parentId.Value)
                .When(dto => dto.Body?.ParentId != null)
                .WithMessage("The project must not be its own parent");
        }
    }

    public class ProjectModifyBodyDtoValidator : AbstractValidator<ProjectModifyBodyDto>
    {
        public ProjectModifyBodyDtoValidator()
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
                .WithMessage("EndDate must be greater than EndDate");
            });
        }
    }
}

using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.TodoDomain.Commands;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class TodoModifyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimatedTime { get; set; }
        public Guid? AssigneeId { get; set; }
        public int? ReminderDaysBefore { get; set; }
    }

    public class TodoModifyDtoMappingProfile : Profile
    {
        public TodoModifyDtoMappingProfile()
        {
            _ = CreateMap<TodoModifyDto, ModifyTodo.Command>();
        }
    }

    public class TodoModifyDtoValidator : AbstractValidator<TodoModifyDto>
    {
        public TodoModifyDtoValidator()
        {
            _ = RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(500);

            _ = RuleFor(p => p.Description)
                .MaximumLength(4000);

            _ = RuleFor(p => p.Priority)
                .IsInEnum();

            _ = RuleFor(p => p.ReminderDaysBefore)
                .InclusiveBetween(1, 365)
                .When(p => p.ReminderDaysBefore.HasValue);
        }
    }
}

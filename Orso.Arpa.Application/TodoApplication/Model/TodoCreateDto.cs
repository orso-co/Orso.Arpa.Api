using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.TodoDomain.Commands;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class TodoCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoPriority? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimatedTime { get; set; }
        public int SortOrder { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? ParentTodoId { get; set; }
        public TodoEntityType EntityType { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? OrganizationId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? SourceChatMessageId { get; set; }
        public int? ReminderDaysBefore { get; set; }
    }

    public class TodoCreateDtoMappingProfile : Profile
    {
        public TodoCreateDtoMappingProfile()
        {
            _ = CreateMap<TodoCreateDto, CreateTodo.Command>();
        }
    }

    public class TodoCreateDtoValidator : AbstractValidator<TodoCreateDto>
    {
        public TodoCreateDtoValidator()
        {
            _ = RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(500);

            _ = RuleFor(p => p.Description)
                .MaximumLength(4000);

            _ = RuleFor(p => p.Priority)
                .IsInEnum()
                .When(p => p.Priority.HasValue);

            _ = RuleFor(p => p.EntityType)
                .IsInEnum();

            _ = RuleFor(p => p.ReminderDaysBefore)
                .InclusiveBetween(1, 365)
                .When(p => p.ReminderDaysBefore.HasValue);
        }
    }
}

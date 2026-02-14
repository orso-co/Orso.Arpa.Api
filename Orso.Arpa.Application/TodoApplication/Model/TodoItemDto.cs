using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.TodoDomain.Enums;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class TodoItemDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? EstimatedTime { get; set; }
        public int SortOrder { get; set; }

        public Guid CreatorId { get; set; }
        public string CreatorDisplayName { get; set; }
        public Guid? AssigneeId { get; set; }
        public string AssigneeDisplayName { get; set; }

        public Guid? ParentTodoId { get; set; }
        public TodoEntityType EntityType { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? OrganizationId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AppointmentId { get; set; }

        public Guid? ChatRoomId { get; set; }
        public Guid? SourceChatMessageId { get; set; }

        public int? ReminderDaysBefore { get; set; }
        public DateTime? ReminderSentAt { get; set; }

        public IList<TodoItemDto> SubTasks { get; set; } = [];
        public IList<TodoCommentDto> Comments { get; set; } = [];
        public int SubTaskCount { get; set; }
        public int CommentCount { get; set; }
    }

    public class TodoItemDtoMappingProfile : Profile
    {
        public TodoItemDtoMappingProfile()
        {
            _ = CreateMap<TodoItem, TodoItemDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.CreatorDisplayName, opt => opt.MapFrom(src =>
                    src.Creator != null && src.Creator.Person != null
                        ? src.Creator.Person.GivenName + " " + src.Creator.Person.Surname
                        : src.CreatedBy))
                .ForMember(dest => dest.AssigneeDisplayName, opt => opt.MapFrom(src =>
                    src.Assignee != null && src.Assignee.Person != null
                        ? src.Assignee.Person.GivenName + " " + src.Assignee.Person.Surname
                        : null))
                .ForMember(dest => dest.SubTaskCount, opt => opt.MapFrom(src => src.SubTasks != null ? src.SubTasks.Count : 0))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Count : 0));
        }
    }
}

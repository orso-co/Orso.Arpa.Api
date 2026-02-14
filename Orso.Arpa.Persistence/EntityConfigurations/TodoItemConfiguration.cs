using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            _ = builder
                .Property(e => e.Title)
                .HasMaxLength(500)
                .IsRequired();

            _ = builder
                .Property(e => e.Description)
                .HasMaxLength(4000);

            _ = builder
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.Priority)
                .HasConversion<string>()
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.EntityType)
                .HasConversion<string>()
                .HasMaxLength(50);

            // Indexes
            _ = builder.HasIndex(e => e.Status);
            _ = builder.HasIndex(e => e.Priority);
            _ = builder.HasIndex(e => e.DueDate);
            _ = builder.HasIndex(e => e.CreatorId);
            _ = builder.HasIndex(e => e.AssigneeId);
            _ = builder.HasIndex(e => e.EntityType);
            _ = builder.HasIndex(e => e.PersonId);
            _ = builder.HasIndex(e => e.OrganizationId);
            _ = builder.HasIndex(e => e.ProjectId);
            _ = builder.HasIndex(e => e.AppointmentId);

            // Creator
            _ = builder
                .HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Assignee
            _ = builder
                .HasOne(e => e.Assignee)
                .WithMany()
                .HasForeignKey(e => e.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Subtask hierarchy
            _ = builder
                .HasOne(e => e.ParentTodo)
                .WithMany(e => e.SubTasks)
                .HasForeignKey(e => e.ParentTodoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Entity linking
            _ = builder
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Organization)
                .WithMany()
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Appointment)
                .WithMany()
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Chat integration
            _ = builder
                .HasOne(e => e.ChatRoom)
                .WithMany()
                .HasForeignKey(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.SetNull);

            _ = builder
                .HasOne(e => e.SourceChatMessage)
                .WithMany()
                .HasForeignKey(e => e.SourceChatMessageId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

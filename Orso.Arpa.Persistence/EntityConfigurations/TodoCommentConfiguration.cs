using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TodoCommentConfiguration : IEntityTypeConfiguration<TodoComment>
    {
        public void Configure(EntityTypeBuilder<TodoComment> builder)
        {
            _ = builder
                .Property(e => e.Content)
                .HasMaxLength(2000)
                .IsRequired();

            _ = builder
                .HasOne(e => e.TodoItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(e => e.TodoItemId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder
                .HasOne(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasIndex(e => e.TodoItemId);
        }
    }
}

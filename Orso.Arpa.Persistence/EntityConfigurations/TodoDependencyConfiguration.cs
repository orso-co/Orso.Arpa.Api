using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TodoDependencyConfiguration : IEntityTypeConfiguration<TodoDependency>
    {
        public void Configure(EntityTypeBuilder<TodoDependency> builder)
        {
            _ = builder
                .HasKey(e => new { e.DependentTaskId, e.DependsOnTaskId });

            _ = builder
                .Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(50);

            _ = builder
                .HasOne(e => e.DependentTask)
                .WithMany(t => t.DependentOn)
                .HasForeignKey(e => e.DependentTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder
                .HasOne(e => e.DependsOnTask)
                .WithMany(t => t.DependedOnBy)
                .HasForeignKey(e => e.DependsOnTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

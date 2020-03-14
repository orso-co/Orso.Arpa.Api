using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // ToDo: delete cascade in code
            builder
                .HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Genre)
                .WithMany(g => g.ProjectsAsGenre)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(e => e.Title)
                .HasMaxLength(50);

            builder
                .Property(e => e.Description)
                .HasMaxLength(1000);
        }
    }
}

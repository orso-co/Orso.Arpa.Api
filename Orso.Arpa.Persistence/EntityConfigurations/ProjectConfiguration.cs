using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .Property(e => e.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.ShortTitle)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasMaxLength(1000);

            builder
                .Property(e => e.Code)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .HasIndex(e => e.Code);

            builder
                .HasOne(e => e.Type)
                .WithMany(g => g.ProjectsAsType)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Genre)
                .WithMany(g => g.ProjectsAsGenre)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.State)
                .WithMany(g => g.ProjectsAsState)
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

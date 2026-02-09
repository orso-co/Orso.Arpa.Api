using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatFolderConfiguration : IEntityTypeConfiguration<ChatFolder>
    {
        public void Configure(EntityTypeBuilder<ChatFolder> builder)
        {
            _ = builder
                .Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            _ = builder
                .Property(e => e.IsSystem)
                .IsRequired();

            _ = builder
                .Property(e => e.SortOrder)
                .IsRequired()
                .HasDefaultValue(0);

            // Index for owner lookups (find all personal folders for a user)
            _ = builder.HasIndex(e => e.OwnerId);

            // Index for system folder queries
            _ = builder.HasIndex(e => e.IsSystem);

            // Relationship to Owner (User)
            _ = builder
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Self-referential relationship for nesting
            _ = builder
                .HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

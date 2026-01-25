using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceFileRoleConfiguration : IEntityTypeConfiguration<MusicPieceFileRole>
    {
        public void Configure(EntityTypeBuilder<MusicPieceFileRole> builder)
        {
            _ = builder
                .HasOne(e => e.MusicPieceFile)
                .WithMany(f => f.Roles)
                .HasForeignKey(e => e.MusicPieceFileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            _ = builder.HasIndex(e => new { e.MusicPieceFileId, e.RoleId }).IsUnique();
        }
    }
}

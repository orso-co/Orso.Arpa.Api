using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceTodoConfiguration : IEntityTypeConfiguration<MusicPieceTodo>
    {
        public void Configure(EntityTypeBuilder<MusicPieceTodo> builder)
        {
            builder.Property(e => e.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(e => e.MusicPiece)
                .WithMany(p => p.Todos)
                .HasForeignKey(e => e.MusicPieceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // Many-to-many relationship with Users (assignees)
            builder.HasMany(e => e.Assignees)
                .WithMany()
                .UsingEntity(
                    "MusicPieceTodoAssignee",
                    l => l.HasOne(typeof(Orso.Arpa.Domain.UserDomain.Model.User)).WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne(typeof(MusicPieceTodo)).WithMany().HasForeignKey("MusicPieceTodoId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasKey("MusicPieceTodoId", "UserId"));

            builder.HasIndex(e => e.MusicPieceId);
            builder.HasIndex(e => e.IsCompleted);
            builder.HasIndex(e => e.DueDate);
        }
    }
}

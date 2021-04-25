using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class MusicianProfileCredentialConfiguration : IEntityTypeConfiguration<MusicianProfileReference>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileReference> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.ReferenceId });

            builder
                .HasOne(e => e.Reference)
                .WithMany(r => r.MusicianProfileReferences)
                .HasForeignKey(e => e.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.MusicianProfileReferences)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}

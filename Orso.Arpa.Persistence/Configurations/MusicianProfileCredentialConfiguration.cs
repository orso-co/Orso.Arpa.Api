using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class MusicianProfileCredentialConfiguration : IEntityTypeConfiguration<MusicianProfileCredential>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileCredential> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.CredentialId });

            builder
                .HasOne(e => e.Credential)
                .WithMany(r => r.MusicianProfileCredentials)
                .HasForeignKey(e => e.CredentialId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.MusicianProfileCredentials)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}

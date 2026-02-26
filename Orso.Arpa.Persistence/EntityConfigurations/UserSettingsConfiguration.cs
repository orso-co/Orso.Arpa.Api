using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasKey(e => e.UserId);

            builder
                .Property(e => e.IsDarkMode)
                .HasDefaultValue(true);

            builder
                .Property(e => e.Language)
                .HasMaxLength(10)
                .HasDefaultValue("de");

            builder
                .Property(e => e.SoundOnUserOnline)
                .HasDefaultValue(false);

            builder
                .Property(e => e.SoundOnAnnouncement)
                .HasDefaultValue(false);

            builder
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<UserSettings>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

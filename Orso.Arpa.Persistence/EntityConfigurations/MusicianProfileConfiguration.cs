using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileConfiguration : IEntityTypeConfiguration<MusicianProfile>
    {
        public void Configure(EntityTypeBuilder<MusicianProfile> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.MusicianProfiles)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Instrument)
                .WithMany(s => s.MusicianProfiles)
                .HasForeignKey(e => e.InstrumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Qualification)
                .WithMany()
                .HasForeignKey(e => e.QualificationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Salary)
                .WithMany()
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.InquiryStatusInner)
                .WithMany()
                .HasForeignKey(e => e.InquiryStatusInnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.InquiryStatusTeam)
                .WithMany()
                .HasForeignKey(e => e.InquiryStatusTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.BackgroundInner)
                .HasMaxLength(1000);

            builder
                .Property(e => e.BackgroundTeam)
                .HasMaxLength(1000);

            builder
                .Property(e => e.SalaryComment)
                .HasMaxLength(500);
        }
    }
}

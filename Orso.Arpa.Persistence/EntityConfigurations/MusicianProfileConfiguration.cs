using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileConfiguration : IEntityTypeConfiguration<MusicianProfile>
    {
        public void Configure(EntityTypeBuilder<MusicianProfile> builder)
        {
            _ = builder
                .HasOne(e => e.Person)
                .WithMany(p => p.MusicianProfiles)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Instrument)
                .WithMany(s => s.MusicianProfiles)
                .HasForeignKey(e => e.InstrumentId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Qualification)
                .WithMany()
                .HasForeignKey(e => e.QualificationId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Salary)
                .WithMany()
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.BackgroundInner)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.BackgroundTeam)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.SalaryComment)
                .HasMaxLength(500);

            _ = builder
                .Property(s => s.InquiryStatusInner)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder
                .Property(s => s.InquiryStatusTeam)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder.HasIndex(e => e.InquiryStatusInner);

            _ = builder.HasIndex(e => e.InquiryStatusTeam);
        }
    }
}

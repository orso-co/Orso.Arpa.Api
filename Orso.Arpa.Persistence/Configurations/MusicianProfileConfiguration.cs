using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
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
                .WithMany(c => c.MusicianProfilesAsQualification)
                .HasForeignKey(e => e.QualificationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Salary)
                .WithMany(c => c.MusicianProfilesAsSalary)
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.InquiryStatus)
                .WithMany(c => c.MusicianProfilesAsInquiryStatus)
                .HasForeignKey(e => e.InquiryStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.Background)
                .HasMaxLength(1000);
        }
    }
}

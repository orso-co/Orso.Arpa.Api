using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            _ = builder
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Salary)
                .WithMany()
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.SalaryPattern)
                .WithMany()
                .HasForeignKey(e => e.SalaryPatternId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Expectation)
                .WithMany()
                .HasForeignKey(e => e.ExpectationId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Venue)
                .WithMany(c => c.Appointments)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.PublicDetails)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.InternalDetails)
                .HasMaxLength(1000);

            _ = builder
                .Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder.HasIndex(e => e.Status);
        }
    }
}

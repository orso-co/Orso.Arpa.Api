using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.AppointmentsAsCategory)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Status)
                .WithMany(c => c.AppointmentsAsStatus)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Emolument)
                .WithMany(c => c.AppointmentsAsEmolument)
                .HasForeignKey(e => e.EmolumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.EmolumentPattern)
                .WithMany(c => c.AppointmentsAsEmolumentPattern)
                .HasForeignKey(e => e.EmolumentPatternId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Expectation)
                .WithMany(c => c.AppointmentsAsExpectation)
                .HasForeignKey(e => e.ExpectationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Venue)
                .WithMany(c => c.Appointments)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.PublicDetails)
                .HasMaxLength(1000);

            builder
                .Property(e => e.InternalDetails)
                .HasMaxLength(1000);
        }
    }
}

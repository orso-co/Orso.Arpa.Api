using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
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
                .HasOne(e => e.Salary)
                .WithMany(c => c.AppointmentsAsSalary)
                .HasForeignKey(e => e.SalaryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.SalaryPattern)
                .WithMany(c => c.AppointmentsAsSalaryPattern)
                .HasForeignKey(e => e.SalaryPatternId)
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

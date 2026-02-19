using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class SurveyUserResponseConfiguration : IEntityTypeConfiguration<SurveyUserResponse>
{
    public void Configure(EntityTypeBuilder<SurveyUserResponse> builder)
    {
        _ = builder
            .HasIndex(e => new { e.SurveyId, e.UserId })
            .IsUnique()
            .HasFilter("deleted = false");

        _ = builder
            .Property(e => e.IpAddress)
            .HasMaxLength(50);

        _ = builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        _ = builder
            .HasMany(e => e.Answers)
            .WithOne(a => a.Response)
            .HasForeignKey(a => a.ResponseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

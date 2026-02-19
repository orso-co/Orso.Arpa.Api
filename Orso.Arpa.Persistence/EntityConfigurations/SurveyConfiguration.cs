using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        _ = builder
            .Property(e => e.Title)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.Description)
            .HasMaxLength(1000);

        _ = builder
            .HasIndex(e => e.IsActive);

        _ = builder
            .HasMany(e => e.Questions)
            .WithOne(q => q.Survey)
            .HasForeignKey(q => q.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder
            .HasMany(e => e.UserResponses)
            .WithOne(r => r.Survey)
            .HasForeignKey(r => r.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

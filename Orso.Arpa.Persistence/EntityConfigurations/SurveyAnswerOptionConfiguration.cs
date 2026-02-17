using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class SurveyAnswerOptionConfiguration : IEntityTypeConfiguration<SurveyAnswerOption>
{
    public void Configure(EntityTypeBuilder<SurveyAnswerOption> builder)
    {
        _ = builder
            .Property(e => e.OptionText)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.Value)
            .HasMaxLength(200);
    }
}

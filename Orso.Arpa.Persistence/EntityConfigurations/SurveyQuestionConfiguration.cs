using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
    {
        _ = builder
            .Property(e => e.QuestionText)
            .HasMaxLength(500)
            .IsRequired();

        _ = builder
            .Property(e => e.QuestionType)
            .HasConversion<int>();

        _ = builder
            .Property(e => e.Settings)
            .HasMaxLength(2000);

        _ = builder
            .Property(e => e.ValidationRules)
            .HasMaxLength(2000);

        _ = builder
            .HasMany(e => e.AnswerOptions)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

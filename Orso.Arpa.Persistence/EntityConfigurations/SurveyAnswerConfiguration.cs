using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        _ = builder
            .Property(e => e.AnswerValue)
            .HasMaxLength(4000);

        _ = builder
            .HasOne(e => e.Question)
            .WithMany()
            .HasForeignKey(e => e.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

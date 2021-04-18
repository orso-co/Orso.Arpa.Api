using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.SelectValueCategories;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class SelectValueCategoryConfiguration : IEntityTypeConfiguration<SelectValueCategory>
    {
        public void Configure(EntityTypeBuilder<SelectValueCategory> builder)
        {
            builder
                .HasData(SelectValueCategorySeedData.SelectValueCategories);

            builder.HasIndex(e => e.Table);
            builder.HasIndex(e => e.Property);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.Table)
                .HasMaxLength(50);

            builder
                .Property(e => e.Property)
                .HasMaxLength(50);
        }
    }
}

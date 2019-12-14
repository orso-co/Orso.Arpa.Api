using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.SelectValueCategories;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SelectValueCategoryConfiguration : IEntityTypeConfiguration<SelectValueCategory>
    {
        public void Configure(EntityTypeBuilder<SelectValueCategory> builder)
        {
            builder
                .HasData(SelectValueCategorySeedData.SelectValueCategories);

            builder.HasIndex(e => e.Table);
            builder.HasIndex(e => e.Property);
        }
    }
}

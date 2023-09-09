using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Util;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeSelectValueCategories
    {
        public static IList<SelectValueCategory> SelectValueCategories
        {
            get
            {
                IList<SelectValueCategory> categories = SelectValueCategorySeedData.SelectValueCategories;
                IList<SelectValueMapping> mappings = SelectValueMappingSeedData.SelectValueMappings;

                foreach (SelectValueCategory category in categories)
                {
                    foreach (SelectValueMapping mapping in mappings.Where(m => m.SelectValueCategoryId == category.Id))
                    {
                        category.SelectValueMappings.Add(mapping);
                    }
                }
                return categories;
            }
        }
    }
}

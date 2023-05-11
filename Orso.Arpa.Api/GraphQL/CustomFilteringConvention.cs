using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;

namespace Orso.Arpa.Api.GraphQL
{
    public class CustomFilteringConvention : FilterConvention
    {
        protected override void Configure(IFilterConventionDescriptor descriptor)
        {
            _ = descriptor.AddDefaults();
            _ = descriptor.Operation(DefaultFilterOperations.Equals).Name("equals");
            _ = descriptor.AddProviderExtension(
                new QueryableFilterProviderExtension(x =>
                {
                    _ = x.AddFieldHandler<QueryableStringInvariantEqualsHandler>()
                     .AddFieldHandler<QueryableStringInvariantContainsHandler>();
                }));
        }
    }
}

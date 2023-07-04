using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Orso.Arpa.Persistence.GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        protected override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<GraphQLContext>();
        }
    }
}

using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Orso.Arpa.Persistence.GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<GraphQLContext>();
        }
    }
}

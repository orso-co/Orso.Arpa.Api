using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Orso.Arpa.Api.ModelBinding
{
    public class BodyAndRouteBindingSource : BindingSource
    {
        public static readonly BindingSource BodyAndRoute = new BodyAndRouteBindingSource(
            "BodyAndRoute",
            "BodyAndRoute",
            true,
            true
            );

        public BodyAndRouteBindingSource(
            string id,
            string displayName,
            bool isGreedy,
            bool isFromRequest) : base(id, displayName, isGreedy, isFromRequest)
        {
        }

        public override bool CanAcceptDataFrom(BindingSource bindingSource)
        {
            return bindingSource == Body || bindingSource == this;
        }
    }
}

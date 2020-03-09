using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Orso.Arpa.Api.ModelBinding
{
    public class BodyAndRouteModelBinderProvider : IModelBinderProvider
    {
        private readonly BodyModelBinderProvider _bodyModelBinderProvider;
        private readonly ComplexTypeModelBinderProvider _complexTypeModelBinderProvider;

        public BodyAndRouteModelBinderProvider(
            BodyModelBinderProvider bodyModelBinderProvider,
            ComplexTypeModelBinderProvider complexTypeModelBinderProvider)
        {
            _bodyModelBinderProvider = bodyModelBinderProvider;
            _complexTypeModelBinderProvider = complexTypeModelBinderProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder bodyBinder = _bodyModelBinderProvider.GetBinder(context);
            IModelBinder complexBinder = _complexTypeModelBinderProvider.GetBinder(context);

            if (context.BindingInfo.BindingSource?.CanAcceptDataFrom(BodyAndRouteBindingSource.BodyAndRoute) == true)
            {
                return new BodyAndRouteModelBinder(bodyBinder, complexBinder);
            }
            else
            {
                return null;
            }
        }
    }
}

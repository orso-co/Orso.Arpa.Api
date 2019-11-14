using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Api.ModelBinders
{
    public class ModifyDtoModelBinder<TDto> : IModelBinder where TDto : IModifyDto
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (!Guid.TryParse(bindingContext.ValueProvider.GetValue("id").FirstValue, out Guid id))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var serializedDto = bindingContext.ValueProvider.GetValue(string.Empty).FirstValue;
            TDto dto = JsonConvert.DeserializeObject<TDto>(serializedDto);

            dto.Id = id;

            bindingContext.Result = ModelBindingResult.Success(dto);

            return Task.CompletedTask;
        }
    }
}

using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Orso.Arpa.Application.General
{
    public abstract class IdFromRouteDto<T>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromBody]
        public T Body { get; set; }
    }

    public class BaseModifyDtoValidator<T, TBody> : AbstractValidator<T> where T : IdFromRouteDto<TBody>
    {
        public BaseModifyDtoValidator()
        {
            RuleFor(b => b).NotNull();
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.Body).NotEmpty();
        }
    }
}

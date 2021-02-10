using System;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilder<TRequest, Guid> ruleBuilder,
            IArpaContext arpaContext) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                    .MustAsync(async (id, cancellation) => await arpaContext.Set<TEntity>()
                        .AnyAsync(entity => entity.Id == id, cancellation))
                    .WithMessage($"The {typeof(TEntity).Name} could not be found.");
        }

        public static IRuleBuilder<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilder<TRequest, Guid?> ruleBuilder,
            IArpaContext arpaContext) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                    .MustAsync(async (id, cancellation) => id == null || await arpaContext.Set<TEntity>()
                        .AnyAsync(entity => entity.Id == id.Value, cancellation))
                    .WithMessage($"The {typeof(TEntity).Name} could not be found.");
        }
    }
}

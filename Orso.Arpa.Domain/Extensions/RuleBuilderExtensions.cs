using System;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilder<TRequest, Guid> ruleBuilder,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                    .MustAsync(async (id, cancellation) => await arpaContext.Set<TEntity>()
                        .AnyAsync(entity => entity.Id == id, cancellation))
                    .OnFailure(request => throw new NotFoundException(nameof(Appointment), propertyName, request));
        }

        public static IRuleBuilder<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilder<TRequest, Guid?> ruleBuilder,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                    .MustAsync(async (id, cancellation) => id == null || await arpaContext.Set<TEntity>()
                        .AnyAsync(entity => entity.Id == id.Value, cancellation))
                    .OnFailure(request => throw new NotFoundException(nameof(Appointment), propertyName, request));
        }
    }
}

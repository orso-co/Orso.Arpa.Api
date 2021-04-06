using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using MediatR;
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
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .WithMessage(GetEntityExistsErrorMessage<TEntity>());
        }

        public static IRuleBuilder<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilder<TRequest, Guid?> ruleBuilder,
            IArpaContext arpaContext) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                .MustAsync(async (id, cancellation) => !id.HasValue || (await arpaContext.EntityExistsAsync<TEntity>(id.Value, cancellation)))
                .WithMessage(GetEntityExistsErrorMessage<TEntity>());
        }

        public static IRuleBuilderOptions<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => !id.HasValue || (await arpaContext.EntityExistsAsync<TEntity>(id.Value, cancellation)))
                .WithMessage(GetEntityExistsErrorMessage<TEntity>());
        }

        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .WithMessage(GetEntityExistsErrorMessage<TEntity>());
        }

        private static string GetEntityExistsErrorMessage<TEntity>()
        {
            return $"The {typeof(TEntity).Name} could not be found.";
        }

        public static IRuleBuilderOptions<TRequest, Guid?> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, SelectValueMapping>> propertyPath) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            var member = propertyPath.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;

            return ruleBuilderInitial
                .EntityExists<TRequest, SelectValueMapping>(arpaContext)
                .MustAsync(async (selectValueMappingId, cancellation) => !selectValueMappingId.HasValue || (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property == propInfo.Name, cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId.Value))
                .WithMessage("The selected value is not valid for this field");
        }
    }
}

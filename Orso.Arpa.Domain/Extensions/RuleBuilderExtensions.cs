using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitialCollection<TRequest, Guid> ruleBuilder,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IRequest where TEntity : BaseEntity
        {
            return ruleBuilder
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .OnFailure((request) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => !id.HasValue || (await arpaContext.EntityExistsAsync<TEntity>(id.Value, cancellation)))
                .OnFailure((request) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .OnFailure((request) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid?> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, SelectValueMapping>> propertyPath) where TRequest : IBaseRequest where TEntity : BaseEntity
        {
            var propertyName = GetPropertyNameFromExpression(propertyPath.Body);

            return ruleBuilderInitial
                .EntityExists<TRequest, SelectValueMapping>(arpaContext, propertyName)
                .MustAsync(async (selectValueMappingId, cancellation) => !selectValueMappingId.HasValue || (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property == propertyName, cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId.Value))
                .WithMessage("The selected value is not valid for this field");
        }

        private static string GetPropertyNameFromExpression(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            var propInfo = memberExpression.Member as PropertyInfo;
            return propInfo.Name;
        }

    }
}

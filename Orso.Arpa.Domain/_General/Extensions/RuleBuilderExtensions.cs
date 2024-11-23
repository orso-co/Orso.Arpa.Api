using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.Logic.MusicianProfileDomain.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.General.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitialCollection<TRequest, Guid> ruleBuilder,
            IArpaContext arpaContext) where TEntity : BaseEntity
        {
            return ruleBuilder
                .MustAsync(arpaContext.EntityExistsAsync<TEntity>)
                .WithErrorCode("404")
                .WithMessage($"{typeof(TEntity).Name} could not be found.");
        }

        public static IRuleBuilderOptions<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext) where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => !id.HasValue || (await arpaContext.EntityExistsAsync<TEntity>(id.Value, cancellation)))
                .WithErrorCode("404")
                .WithMessage($"{typeof(TEntity).Name} could not be found.");
        }

        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext) where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .WithErrorCode("404")
                .WithMessage($"{typeof(TEntity).Name} could not be found.");
        }

        public static IRuleBuilderOptions<TRequest, Guid?> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, object>> propertyPath) where TEntity : BaseEntity
        {
            var propertyName = GetPropertyNameFromExpression(propertyPath.Body);

            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueMapping>(arpaContext)
                .MustAsync(async (selectValueMappingId, cancellation) => !selectValueMappingId.HasValue || (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property.Equals(propertyName), cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId.Value))
                .WithMessage("The selected value is not valid for this field");
        }

        public static IRuleBuilderOptions<TRequest, Guid> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, object>> propertyPath) where TEntity : BaseEntity
        {
            var propertyName = GetPropertyNameFromExpression(propertyPath.Body);

            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueMapping>(arpaContext)
                .MustAsync(async (selectValueMappingId, cancellation) => (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property.Equals(propertyName), cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId))
                .WithMessage("The selected value is not valid for this field");
        }

        public static IRuleBuilderOptions<TRequest, Guid> MusicianProfilePosition<TRequest>(
            this IRuleBuilderInitialCollection<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext) where TRequest : IHasInstrumentRequest
        {
            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueSection>(arpaContext)
                .MustAsync(async (request, selectValueSectionId, cancellation) => (await arpaContext
                    .FindAsync<Section>(new object[] { request.InstrumentId }, cancellation))
                    .SelectValueSections.Any(item => item.Id.Equals(selectValueSectionId)))
                .WithMessage("The selected position is not valid for this instrument");
        }

        public static IRuleBuilderOptions<TRequest, byte> InstrumentPart<TRequest>(
            this IRuleBuilderInitialCollection<TRequest, byte> ruleBuilderInitial,
            IArpaContext arpaContext) where TRequest : IHasInstrumentRequest
        {
            return ruleBuilderInitial
                .MustAsync(async (request, preferredPart, cancellation) => (await arpaContext
                    .FindAsync<Section>(new object[] { request.InstrumentId }, cancellation))
                    .InstrumentPartCount >= preferredPart)
                .WithMessage("The selected part is not valid for this instrument");
        }

        private static string GetPropertyNameFromExpression(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            var propInfo = memberExpression.Member as PropertyInfo;
            return propInfo.Name;
        }
    }
}

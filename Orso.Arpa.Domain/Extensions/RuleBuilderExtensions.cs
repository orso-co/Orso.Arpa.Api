using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitialCollection<TRequest, Guid> ruleBuilder,
            IArpaContext arpaContext,
            string propertyName) where TEntity : BaseEntity
        {
            return ruleBuilder
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .OnFailure((_) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid?> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            string propertyName) where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => !id.HasValue || (await arpaContext.EntityExistsAsync<TEntity>(id.Value, cancellation)))
                .OnFailure((_) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid> EntityExists<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            string propertyName) where TEntity : BaseEntity
        {
            return ruleBuilderInitial
                .MustAsync(async (id, cancellation) => (await arpaContext.EntityExistsAsync<TEntity>(id, cancellation)))
                .OnFailure((_) => throw new NotFoundException(typeof(TEntity).Name, propertyName));
        }

        public static IRuleBuilderOptions<TRequest, Guid?> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid?> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, SelectValueMapping>> propertyPath) where TEntity : BaseEntity
        {
            var propertyName = GetPropertyNameFromExpression(propertyPath.Body);

            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueMapping>(arpaContext, propertyName)
                .MustAsync(async (selectValueMappingId, cancellation) => !selectValueMappingId.HasValue || (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property.Equals(propertyName), cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId.Value))
                .WithMessage("The selected value is not valid for this field");
        }

        public static IRuleBuilderOptions<TRequest, Guid> SelectValueMapping<TRequest, TEntity>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            Expression<Func<TEntity, SelectValueMapping>> propertyPath) where TEntity : BaseEntity
        {
            var propertyName = GetPropertyNameFromExpression(propertyPath.Body);

            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueMapping>(arpaContext, propertyName)
                .MustAsync(async (selectValueMappingId, cancellation) => (await arpaContext.SelectValueCategories
                    .SingleAsync(category => category.Table == typeof(TEntity).Name && category.Property.Equals(propertyName), cancellation))
                    .SelectValueMappings.Any(mapping => mapping.Id == selectValueMappingId))
                .WithMessage("The selected value is not valid for this field");
        }

        public static IRuleBuilderOptions<TRequest, Guid> MusicianProfilePosition<TRequest>(
            this IRuleBuilderInitialCollection<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            string propertyName) where TRequest : IHasInstrumentRequest
        {
            return ruleBuilderInitial
                .Cascade(CascadeMode.Stop)
                .EntityExists<TRequest, SelectValueSection>(arpaContext, propertyName)
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

        public static IRuleBuilderOptions<TRequest, Guid> DoublingInstrument<TRequest>(
            this IRuleBuilderInitial<TRequest, Guid> ruleBuilderInitial,
            IArpaContext arpaContext,
            Guid mainInstrumentId)
        {
            return ruleBuilderInitial
                .Must(instrumentId => instrumentId != mainInstrumentId)
                .WithMessage("The doubling instrument may not be the main instrument")
                .MustAsync(async (command, instrumentId, cancellation) => await IsValidDoublingInstrumentAsync(mainInstrumentId, instrumentId, arpaContext, cancellation))
                .WithMessage("This instrument is no valid doubling instrument for the selected main instrument");
        }

        private static string GetPropertyNameFromExpression(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            var propInfo = memberExpression.Member as PropertyInfo;
            return propInfo.Name;
        }

        private static async Task<bool> IsValidDoublingInstrumentAsync(Guid mainInstrumentId, Guid doublingInstrumentId, IArpaContext arpaContext, CancellationToken cancellationToken)
        {
            Section mainInstrument = await arpaContext.FindAsync<Section>(new object[] { mainInstrumentId }, cancellationToken);
            if (mainInstrument.IsInstrument)
            {
                return mainInstrument.Children.Select(c => c.Id).Contains(doublingInstrumentId);
            }
            if (mainInstrument.ParentId == doublingInstrumentId)
            {
                return true;
            }
            return mainInstrument.Parent.Children.Select(c => c.Id).Contains(doublingInstrumentId);
        }
    }
}

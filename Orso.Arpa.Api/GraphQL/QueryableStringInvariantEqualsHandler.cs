using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace Orso.Arpa.Api.GraphQL
{
    public class QueryableStringInvariantEqualsHandler : QueryableStringOperationHandler
    {
        private static readonly MethodInfo _toLower = typeof(string)
            .GetMethods()
            .Single(
                x => x.Name == nameof(string.ToLower) &&
                     x.GetParameters().Length == 0);

        public QueryableStringInvariantEqualsHandler(InputParser inputParser) : base(inputParser)
        {
        }

        protected override int Operation => DefaultFilterOperations.Equals;

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object parsedValue)
        {
            Expression property = context.GetInstance();
            if (parsedValue is string str)
            {
                return Expression.Equal(
                    Expression.Call(property, _toLower),
                    Expression.Constant(str.ToLower()));
            }

            throw new InvalidOperationException();
        }
    }
}

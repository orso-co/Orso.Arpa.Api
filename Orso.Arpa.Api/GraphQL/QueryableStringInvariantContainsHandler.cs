using System;
using System.Globalization;
using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace Orso.Arpa.Api.GraphQL
{
    public class QueryableStringInvariantContainsHandler : QueryableStringOperationHandler
    {
        public QueryableStringInvariantContainsHandler(InputParser inputParser) : base(inputParser)
        {
        }

        protected override int Operation => DefaultFilterOperations.Contains;

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object parsedValue)
        {
            Expression property = context.GetInstance();
            if (parsedValue is string str)
            {
                // Bad way of doing it but Using ILIKE on DB level is somehow not working.
                // https://github.com/npgsql/efcore.pg/issues/618
                return Expression.AndAlso(
                    Expression.NotEqual(property, Expression.Constant(null, typeof(object))),
                    Expression.NotEqual(
                        Expression.Call(
                            Expression.Constant(CultureInfo.InvariantCulture.CompareInfo),
                            typeof(string).GetMethod(nameof(CompareInfo.IndexOf), [typeof(string), typeof(string), typeof(CompareOptions)]),
                            property,
                            Expression.Constant(str),
                            Expression.Constant(CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase)
                        ),
                        Expression.Constant(-1)
                    )
                );
            }

            throw new InvalidOperationException();
        }
    }
}

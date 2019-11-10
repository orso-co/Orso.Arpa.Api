using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence
{
    public static class ArpaContextUtility
    {
        public const string IsDeletedProperty = nameof(BaseEntity.Deleted);

        public static readonly MethodInfo PropertyMethod = typeof(EF)
            .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)
            .MakeGenericMethod(typeof(bool));

        public static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            ParameterExpression parm = Expression.Parameter(type, "it");
            MethodCallExpression prop = Expression.Call(PropertyMethod, parm, Expression.Constant(IsDeletedProperty));
            BinaryExpression condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            return Expression.Lambda(condition, parm);
        }
    }
}

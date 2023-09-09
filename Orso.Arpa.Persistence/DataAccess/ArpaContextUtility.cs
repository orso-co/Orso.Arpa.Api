using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Persistence.DataAccess
{
    public static class ArpaContextUtility
    {
        private static IList<Type> _entityTypeCache;

        public static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)

            {
                return _entityTypeCache.ToList();
            }

            Assembly assembly = typeof(BaseEntity).Assembly;

            _entityTypeCache = (from t in assembly.DefinedTypes

                                where t.BaseType == typeof(BaseEntity)

                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        public static readonly MethodInfo SetGlobalQueryMethod
            = typeof(ArpaContextUtility).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(t => t.IsGenericMethod && t.Name == nameof(SetGlobalQuery));

        public static void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);

            Debug.WriteLine("Adding global query for: " + typeof(T));

            builder.Entity<T>().HasQueryFilter(e => !e.Deleted);
        }
    }
}

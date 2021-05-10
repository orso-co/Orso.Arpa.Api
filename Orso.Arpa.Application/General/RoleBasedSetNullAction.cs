using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.General
{
    public class RoleBasedSetNullAction<TSource, TDestination> : IMappingAction<TSource, TDestination>
    {
        private readonly ITokenAccessor _tokenAccessor;

        public RoleBasedSetNullAction(ITokenAccessor tokenAccessor)
        {
            _tokenAccessor = tokenAccessor ?? throw new ArgumentNullException(nameof(tokenAccessor));
        }

        public void Process(TSource source, TDestination destination, ResolutionContext context)
        {
            IEnumerable<PropertyInfo> props = typeof(TDestination).GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(IncludeForRolesAttribute)));

            foreach (PropertyInfo prop in props)
            {
                IncludeForRolesAttribute attr = prop.GetCustomAttribute<IncludeForRolesAttribute>();
                if (!_tokenAccessor.UserRoles.Intersect(attr.RoleNames).Any())
                {
                    prop.SetValue(destination, null);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IncludeForRolesAttribute : Attribute
    {
        public IncludeForRolesAttribute(params string[] roleNames)
        {
            RoleNames = roleNames;
        }
        public string[] RoleNames { get; }
    }
}

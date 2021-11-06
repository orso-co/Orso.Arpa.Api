using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutoMapper;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.General
{
    public class LocalizeAction<TSource, TDestination> : IMappingAction<TSource, TDestination>
    {
        private readonly ILocalizerCache _localizerCache;
        private readonly string _uiCulture;

        public LocalizeAction(ILocalizerCache localizerCache)
        {
            _localizerCache = localizerCache;
            _uiCulture = string.IsNullOrEmpty(Thread.CurrentThread.CurrentUICulture.Parent?.Name) ? Thread.CurrentThread.CurrentUICulture.Name : Thread.CurrentThread.CurrentUICulture.Parent.Name;
        }

        public void Process(TSource source, TDestination destination, ResolutionContext context)
        {
            IEnumerable<PropertyInfo> props = typeof(TDestination)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(TranslateAttribute)) && prop.PropertyType == typeof(string));

            foreach (PropertyInfo prop in props)
            {
                var value = prop.GetValue(destination) as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                if (_localizerCache.TryGetTranslation(value, typeof(TDestination).Name, _uiCulture, out var newValue))
                {
                    prop.SetValue(destination, newValue);
                }
            }
        }
    }
}

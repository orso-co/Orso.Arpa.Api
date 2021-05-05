using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using AutoMapper.Internal;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Localization;


namespace Orso.Arpa.Application.Localization
{
    public class LocationResultFilter : IResultFilter
    {
        private readonly LocalizerCache _localizerCache;
        private readonly string _uiCulture;
        private readonly ILogger<LocationResultFilter> _logger;


        public LocationResultFilter(LocalizerCache localizerCache,
        ILogger<LocationResultFilter> logger)
        {
            _localizerCache = localizerCache;
            _uiCulture = Thread.CurrentThread.CurrentUICulture.Name;
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            object obj;

            if (context.HttpContext.Response.ContentType.IsNullOrEmpty() ||
                !context.HttpContext.Response.ContentType.Contains("application/json"))
            {
                return;
            }

            Type contentValueType = context.Result.GetType().GetProperty("Value")?.GetValue(context.Result)?.GetType();

            context.HttpContext.Response.Body.Position = 0;
            var json = new StreamReader(context.HttpContext.Response.Body).ReadToEnd();
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            if (contentValueType!.FullName!.StartsWith(
                "Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable"))
            {

                obj = (object)JsonSerializer.Deserialize(json,
                    typeof(List<>).MakeGenericType(contentValueType.GetGenericArguments()), jsonSerializerOptions);
            }
            else
            {
                obj = (object)JsonSerializer.Deserialize(json, contentValueType, jsonSerializerOptions);
            }

            TranslateObject(obj,8);

            context.HttpContext.Response.Body.Position = 0;
            context.HttpContext.Response.Body.SetLength(0);
            context.HttpContext.Response.Body.Write(JsonSerializer.SerializeToUtf8Bytes(obj));
        }

        public void TranslateObject(object obj, int maxLevels)
        {
            if (maxLevels < 0)
            {
                return;
            }

            try
            {
                if (obj is IList list)
                {
                    foreach (object o in list)
                    {
                        TranslateObject(o, maxLevels - 1);
                    }

                    return;
                }

                obj.GetType().GetProperties().ForAll(p =>
                {
                    if (p.GetIndexParameters().Length == 0 && p.GetValue(obj) != null)
                    {
                        if (!p.GetCustomAttributes<TranslateAttribute>().IsNullOrEmpty() &&
                            p.GetValue(obj) is string)
                        {
                            p.SetValue(obj,
                                _localizerCache.GetTranslation((string)p.GetValue(obj),
                                    p.GetCustomAttributes<TranslateAttribute>().First().ResourceKey, _uiCulture));

                        }
                        else if (!p.GetCustomAttributes<TranslateAttribute>().IsNullOrEmpty() &&
                                 p.GetValue(obj) is string[])
                        {
                            var translatableArray = (string[])p.GetValue(obj);
                            IList<string> newTranslatedArray = new List<string>();
                            for (int i = 0; i < translatableArray!.Length; i++)
                            {
                                newTranslatedArray.Add(_localizerCache.GetTranslation(
                                    (string)p.GetValue(obj),
                                    p.GetCustomAttributes<TranslateAttribute>().First().ResourceKey, _uiCulture));
                            }

                            p.SetValue(obj, newTranslatedArray.ToArray());

                        }
                        else if (!p.PropertyType.IsPrimitive && !p.PropertyType.IsStatic() &&
                                 p.PropertyType != typeof(string) &&
                                 p.PropertyType != typeof(string[]))
                        {
                            TranslateObject(p.GetValue(obj), maxLevels - 1);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                if (!e.Message.IsNullOrEmpty())
                    _logger.LogError($"Error during translation: {e.Message}");
            }
        }
    }
}

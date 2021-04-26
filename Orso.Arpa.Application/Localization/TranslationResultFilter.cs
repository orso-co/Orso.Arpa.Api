using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Tranlation;
using Orso.Arpa.Domain.Entities;


namespace Orso.Arpa.Application.Localization
{
    public class TranslationResultFilter : IResultFilter
    {
        private readonly IStringLocalizer _localizer;


        public TranslationResultFilter(IStringLocalizer<ApplicationResource> localizer)
        {
            _localizer = localizer;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            return;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (!context.HttpContext.Response.ContentType.Contains("application/json"))
            {
                return;
            }

            object obj = context.Result.GetType().
                GetProperty("Value")?.GetValue(context.Result);

            obj = Translate(obj,2);

            context.HttpContext.Response.Body.Position = 0;
            context.HttpContext.Response.Body.SetLength(0);
            context.HttpContext.Response.Body.Write(JsonSerializer.SerializeToUtf8Bytes(obj));
        }

        public object Translate(object obj, int maxLevels)
        {
            object retObject;

            if (maxLevels < 0 || obj == null)
            {
                return obj;
            }

            if (obj.GetType().FullName!.StartsWith("Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable"))
            {
                retObject = new ArrayList();
                IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ((ArrayList)retObject).Add(Translate(enumerator.Current, maxLevels-1));
                }
                return retObject;
            }

            if (obj.GetType().FullName!.StartsWith("System.Collections.Generic.List"))
            {
                retObject = new List<object>();
                IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ((List<object>)retObject).Add(Translate(enumerator.Current, maxLevels-1));
                }
                return retObject;
            }

            if (obj.GetType() is object[])
            {
                retObject = new ArrayList();
                ((object[])obj).ForAll(o =>
                {
                    ((ArrayList)retObject).Add(Translate(o, maxLevels - 1));
                });
                return retObject;
            }

            retObject = FormatterServices.GetUninitializedObject(obj.GetType());

            try
            {

                foreach (FieldInfo f in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy))
                //obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy).ForAll(f =>
                {
                    if (f.GetValue(obj) != null)
                    {
                        if (f.FieldType.IsPrimitive || f.FieldType.IsStatic() || f.FieldType == typeof(string) || f.FieldType == typeof(string[]))
                        {
                            f.SetValue(retObject, f.GetValue(obj));
                        }
                        else
                        {
                            f.SetValue(retObject, Translate(f.GetValue(obj), maxLevels-1));
                        }
                    }
                }
                //);

                foreach (PropertyInfo p in obj.GetType().GetProperties())
                //obj.GetType().GetProperties().ForAll(p =>
                {
                    if (p.GetIndexParameters().Length == 0 && p.GetValue(obj) != null)
                    {
                        if (p.GetCustomAttributes<TranslateAttribute>() is { } translateAttributes &&
                            p.GetValue(obj) is string)
                        {
                            p.SetValue(retObject, localize((string)p.GetValue(obj)));
                        }

                        else if (p.GetCustomAttributes<TranslateAttribute>() is { } translateAttributes2 &&
                                 p.GetValue(obj) is string[])
                        {
                            var tanslationableArray = (string[])p.GetValue(obj);
                            IList<string> newTranslatedArray = new List<string>();
                            for (int i = 0; i < tanslationableArray.Length; i++)
                            {
                                newTranslatedArray.Add(localize(tanslationableArray[i]));
                            }

                            p.SetValue(retObject, newTranslatedArray.ToArray());
                        }
                        else if (p.PropertyType.IsPrimitive || p.PropertyType.IsStatic() || p.PropertyType == typeof(string) || p.PropertyType == typeof(string[]))
                        {
                            p.SetValue(retObject, p.GetValue(obj));
                        }
                        else
                        {
                            p.SetValue(retObject, Translate(p.GetValue(obj), maxLevels-1));
                        }
                    }
                }
                //);
            }
            catch (Exception e)
            {
                return obj;
            }
            return retObject;
        }

        private string localize(string str)
        {
            return str;
        }
    }
}

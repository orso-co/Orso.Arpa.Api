using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Tranlation;
using Orso.Arpa.Domain.Entities;


namespace Orso.Arpa.Application.Localization
{
    public class TranslationResultFilter : IResultFilter
    {
        private IStringLocalizer _localizer;

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
            object obj = context.Result.GetType().
                GetProperty("Value")?.GetValue(context.Result);

            Translate(obj);

            context.HttpContext.Response.Body.Position = 0;
            context.HttpContext.Response.Body.SetLength(0);
            context.HttpContext.Response.Body.Write(JsonSerializer.SerializeToUtf8Bytes(obj));
        }

        private void Translate(object? obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            if (obj is IEnumerable enumerable)
            {
                foreach (object o in enumerable)
                {
                    Translate(o);
                }
            }

            obj.GetType().GetProperties().ForAll(p =>
            {
                try
                {
                    if (p.GetIndexParameters().Length == 0 && p.GetValue(obj) != null)
                    {
                        if (p.GetCustomAttributes<TranslateAttribute>() != null &&
                            p.GetValue(obj) is string)
                        {
                            p.SetValue(obj, transl((string)p.GetValue(obj)));
                        }

                        else if (p.GetCustomAttributes<TranslateAttribute>() != null &&
                                 p.GetValue(obj) is string[])
                        {
                            var tanslationableArray = (string[])p.GetValue(obj);
                            for (int i = 0; i < tanslationableArray.Length; i++)
                            {
                                tanslationableArray[i] = transl(tanslationableArray[i]);
                            }
                        }
                        else
                        {
                            Translate(p.GetValue(obj));
                        }
                    }
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                }
            });

        }

        private string transl(string str)
        {
            return "this was changed";
        }
/*
        PropertyObject[] GetProperties(object? obj)
        {
            var propertyList = new List<PropertyObject>();

            if (obj == null)
            {
                return propertyList.ToArray();
            }

            if (obj.GetType().IsArray)
            {
                object[] objectArray = (object[])obj;
                objectArray.ForAll(o =>
                {
                    o.GetType().GetProperties().ForAll(p =>
                    {
                        if (p.GetIndexParameters().Length == 0)
                        {
                            propertyList.Add(new PropertyObject(p.GetValue(o), p, o));
                            propertyList.AddRange(GetProperties(p.GetValue(o)));
                        }
                    });
                });
            }
            else
            {
                obj.GetType().GetProperties().ForAll(p =>
                {
                    if (p.GetIndexParameters().Length == 0)
                    {
                        propertyList.Add(new PropertyObject(p.GetValue(obj), p, obj));
                        propertyList.AddRange(GetProperties(p.GetValue(obj)));
                    }
                });

                //obj.GetType().GetProperties().ForAll(p => propertyList.AddRange(GetProperties(p.GetValue(obj))));
            }



            return propertyList.ToArray();
        }

        struct PropertyObject
        {
            public object obj;
            public PropertyInfo propInfo;
            public object parentObj;

            public PropertyObject(object obj, PropertyInfo propInfo, object parentObj)
            {
                this.obj = obj;
                this.propInfo = propInfo;
                this.parentObj = parentObj;
            }
        }

*/
    }
}

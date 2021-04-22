using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Application.Tranlation;


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
            object obj = context.Result.GetType().
                GetProperty("Value")?.GetValue(context.Result);

            Translate(obj);

            context.HttpContext.Response.Body.Position = 0;
            context.HttpContext.Response.Body.SetLength(0);
            context.HttpContext.Response.Body.Write(JsonSerializer.SerializeToUtf8Bytes(obj));
        }

        public void Translate(object? obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            if (obj is IEnumerable enumerable)
            {
                IEnumerator enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Translate(enumerator.Current);
                }
                return;
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
                            p.SetValue(obj, "bla"/*transl((string)p.GetValue(obj))*/);
                        }

                        else if (p.GetCustomAttributes<TranslateAttribute>() != null &&
                                 p.GetValue(obj) is string[])
                        {
                            var tanslationableArray = (string[])p.GetValue(obj);
                            for (int i = 0; i < tanslationableArray.Length; i++)
                            {
                                tanslationableArray[i] = (string) transl(tanslationableArray[i]);
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
    }
}

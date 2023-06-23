using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.News
{
    public static class Create
    {
        public class Command : ICreateCommand<Entities.News>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }

        }
    }
}

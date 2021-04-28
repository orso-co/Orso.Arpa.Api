using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class TranslationSeedData
    {
        public static IList<Translation> Translations
        {
            get
            {
                return new List<Translation>
                {
                    new(new Guid("9703B67C-66F6-4F1E-9D14-9242FDA61DB1"), "This request requires a valid JWT access token to be provided", "Please try to login again", "en-US", "ApiResource"),
                    new(new Guid("EA805337-5D56-45ED-8959-2C80B7AEDE49"), "This request requires a valid JWT access token to be provided", "Bitte melde dich erneut an", "de-DE", "ApiResource")
                };
            }
        }
    }
}

using System;

namespace Orso.Arpa.Infrastructure.Localization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TranslateAttribute : Attribute
    {
        public TranslateAttribute(string localizationKey)
        {
            LocalizationKey = localizationKey;
        }

        public string LocalizationKey { get; }
    }
}

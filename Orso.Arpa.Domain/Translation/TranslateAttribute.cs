using System;

namespace Orso.Arpa.Domain.Translation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TranslateAttribute : Attribute
    {
        private readonly string _resourceKey;
        public TranslateAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public string ResourceKey
        {
            get => _resourceKey;
        }
    }
}

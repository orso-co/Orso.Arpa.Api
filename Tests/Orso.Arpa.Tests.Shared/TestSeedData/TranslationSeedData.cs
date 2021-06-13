using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class TranslationSeedData
    {
        public static IList<Localization> Translations
        {
            get
            {
                return new List<Localization>
                {
                    new(new Guid("9703B67C-66F6-4F1E-9D14-9242FDA61DB1"), "Invalid token supplied", "Please try to login again", "en-GB", "Validator"),
                    new(new Guid("EA805337-5D56-45ED-8959-2C80B7AEDE49"), "Invalid token supplied", "Bitte melde dich erneut an", "de-DE", "Validator"),
                    new(new Guid("1774F3A9-224C-465F-9735-D31D5A0EE2CB"), "Violins", "Violinen", "de-DE", "SectionDto"),
                    new(new Guid("A76C488D-752A-4842-AB14-0540F192D35B"), "Flute", "Flöte", "de-DE", "SectionDto"),
                    new (new Guid("5CA45AB8-B6B9-452F-8300-19F6D71B2433"), "Performer", "Künstler", "de-DE", "RoleDto"),
                    new (new Guid("3BB7EAD2-53FF-4448-A7DD-E190DBB17BD5"), "Staff", "Mitarbeiter", "de-DE", "RoleDto"),
                    new (new Guid("C4AEA398-7A73-4130-8C97-BB159020E53A"), "Admin", "Administrator", "de-DE", "RoleDto")
                };
            }
        }
    }
}

using System;
using NUnit.Framework;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    public abstract class DtoMappingProfileTestBase
    {
        protected DateTimeProviderContext _dateTimeProviderContext;

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            _dateTimeProviderContext = new DateTimeProviderContext(new DateTime(2021, 1, 1));
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _dateTimeProviderContext.Dispose();
        }
    }
}

using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.Logic.Urls.Modify;

namespace Orso.Arpa.Domain.Tests.UrlTests.ValidatorTests
{
    [TestFixture]
    public class UrlModifyCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
        }

        // Todo: define tests
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;
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

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Url>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.Id, UrlSeedData.ArpaWebsite.Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Url>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Id, Guid.NewGuid(), nameof(Url));
        }
    }
}

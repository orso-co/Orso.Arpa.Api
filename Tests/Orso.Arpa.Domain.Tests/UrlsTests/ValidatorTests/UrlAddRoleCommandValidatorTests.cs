using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using static Orso.Arpa.Domain.Logic.Urls.AddRole;

namespace Orso.Arpa.Domain.Tests.UrlTests.ValidatorTests
{
    [TestFixture]
    public class UrlAddRoleCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private RoleManager<Role> _roleManager;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _roleManager = new FakeRoleManager();
            _validator = new Validator(_arpaContext, _roleManager);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_AdminRole_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<UrlRole>(Arg.Any<Expression<Func<UrlRole, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Url>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.RoleId, new Command { RoleId = RoleDtoData.Admin.Id, UrlId = UrlDtoData.GoogleDe.Id });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_NonAdminRole_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<UrlRole>(Arg.Any<Expression<Func<UrlRole, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.EntityExistsAsync<Url>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.RoleId, new Command { RoleId = RoleDtoData.Staff.Id, UrlId = UrlDtoData.GoogleDe.Id });
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.RoleId, new Command { RoleId = RoleDtoData.Performer.Id, UrlId = UrlDtoData.GoogleDe.Id });
        }
    }
}

using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Projects.Create;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class ProjectCreateCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Project> _mockProjectDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockProjectDbSet = MockDbSets.Projects;
            _arpaContext.Set<Project>().Returns(_mockProjectDbSet);
            _arpaContext.Projects.Returns(_mockProjectDbSet);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Duplicate_Number()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number, new Command()
            {
                Number = ProjectSeedData.HoorayForHollywood.Number
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Unused_Number()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Number, new Command()
            {
                Number = "New Number"
            });
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Projects.Modify;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class ProjectModifyCommandValidatorTests
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
        }

        [Test]
        public void Should_Have_Validation_Error_If_Duplicate_Number()
        {
            _mockProjectDbSet.AnyAsync(Arg.Any<Expression<Func<Project, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(command => command.Number, new Command() { Id = Guid.Parse("e721f656-046d-4363-8371-e1c90b29a2ab"), Number = ProjectSeedData.HoorayForHollywood.Number  });
           // _validator.ShouldHaveValidationErrorFor(c => c.Number, ProjectSeedData.HoorayForHollywood.Number);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, ProjectSeedData.RockingXMas.Id);
        }
    }
}

using System;
using System.Linq.Expressions;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionCreateCommandValidatorTests
    {
        private Validator _validator;
        private IReadOnlyRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _validator = new Validator(_repository);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            _repository.Exists<Region>(Arg.Any<Expression<Func<Region, bool>>>()).Returns(true);

            _validator.ShouldHaveValidationErrorFor(command => command.Name, RegionSeedData.Stuttgart.Name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _repository.Exists<Region>(Arg.Any<Expression<Func<Region, bool>>>()).Returns(false);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, "Honolulu");
        }
    }
}

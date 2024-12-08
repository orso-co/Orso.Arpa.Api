using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ReducedPersonDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_tokenAccessor);
            services.AddSingleton<RoleBasedSetNullAction<Person, ReducedPersonDto>>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ReducedPersonDtoMappingProfile>();
            });
             ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ITokenAccessor _tokenAccessor = Substitute.For<ITokenAccessor>();

        [Test]
        public void Should_Map_Person_To_ReducedPersonDto()
        {
            // Arrange
            Person person = PersonTestSeedData.Performer;

            var expectedDto = new ReducedPersonDto
            {
                GivenName = person.GivenName,
                Id = person.Id,
                Surname = person.Surname,
                DisplayName = person.DisplayName
            };

            // Act
            ReducedPersonDto dto = _mapper.Map<ReducedPersonDto>(person);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}

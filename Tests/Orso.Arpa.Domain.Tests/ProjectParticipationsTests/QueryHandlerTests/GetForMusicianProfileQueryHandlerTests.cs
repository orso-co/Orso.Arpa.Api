using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.ProjectParticipationsTests.QueryHandlerTests
{
    [TestFixture]
    public class GetForMusicianProfileQueryHandlerTests
    {
        private IArpaContext _arpaContext;
        private GetForMusicianProfile.Handler _handler;

        [SetUp]
        public void Setup()
        {
            var projectParticipations = MockDbSets.ProjectParticipations;
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new GetForMusicianProfile.Handler(_arpaContext);
        }


    }
}

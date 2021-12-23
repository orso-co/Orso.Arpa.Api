//using System.Threading;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using NSubstitute;
//using NUnit.Framework;
//using Orso.Arpa.Domain.Entities;
//using Orso.Arpa.Domain.GenericHandlers;
//using Orso.Arpa.Domain.Interfaces;
//using Orso.Arpa.Tests.Shared.TestSeedData;

//namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
//{
//    [TestFixture]
//    public class CreateHandlerTests
//    {
//        private IArpaContext _arpaContext;
//        private Create.Handler<Appointment> _handler;

//        [SetUp]
//        public void Setup()
//        {
//            _arpaContext = Substitute.For<IArpaContext>();
//            _handler = new Create.Handler<Appointment>(_arpaContext);
//        }

//        [Test]
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "Necessary to be able to mock EntityEntry.Entity")]
//        public async Task Should_Add_Project()
//        {
//            // Arrange
//            Appointment expectedAppointment = AppointmentSeedData.RockingXMasConcert;
//            IStateManager iStateManager = Substitute.For<IStateManager>();
//            Model model = Substitute.For<Model>();

//            EntityEntry<Appointment> returnedEntityEntry = Substitute.For<EntityEntry<Appointment>>(
//                new InternalShadowEntityEntry(
//                    iStateManager,
//                    new EntityType("Appointment", model, ConfigurationSource.Convention)));
//            returnedEntityEntry.Entity.Returns(expectedAppointment);

//            _arpaContext.Add(Arg.Any<Appointment>())
//                    .Returns(returnedEntityEntry);
//            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>())
//                .Returns(1);
//            _arpaContext.Set<Appointment>().Find(Arg.Any<object[]>()).Returns(expectedAppointment);

//            // Act
//            Appointment result = await _handler
//                .Handle(new Logic.Appointments.Create.Command(), new CancellationToken());

//            // Assert
//            result.Should().BeEquivalentTo(expectedAppointment);
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Application.UserApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

using Me = Orso.Arpa.Domain.Logic.Me;

using Users = Orso.Arpa.Domain.Logic.Users;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private IMapper _mapper;
        private IMediator _mediator;
        private IUserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _userAccessor = Substitute.For<IUserAccessor>();
            _userService = new UserService(_mediator, _mapper, _userAccessor);
        }

        [Test]
        public async Task Should_Get_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Users.List.Query>()).Returns(FakeUsers.Users.Where(u => !u.Deleted));
            _mediator.Send(Arg.Any<Users.UserRoles.Query>()).Returns(
                new[] { RoleNames.Performer },
                new[] { RoleNames.Staff },
                new[] { RoleNames.Admin },
                Array.Empty<string>(),
                Array.Empty<string>(),
                Array.Empty<string>());

            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(
                 UserDtoData.Performer,
                 UserDtoData.Staff,
                 UserDtoData.Admin,
                 UserDtoData.UserWithoutRole,
                 UserDtoData.LockedOutUser,
                 UserDtoData.UnconfirmedUser);

            // Act
            IEnumerable<UserDto> dtos = await _userService.GetAsync();

            // Assert
            dtos.Should().BeEquivalentTo(UserDtoData.Users);
        }

        [Test]
        public async Task Should_Get_Profile_Of_Current_User_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Me.Details.Query>()).Returns(FakeUsers.Performer);
            MyProfileDto expectedDto = FakerFabric.UesrProfileDtoFaker.Generate();
            _mapper.Map<MyProfileDto>(Arg.Any<User>()).Returns(expectedDto);

            // Act
            MyProfileDto profileDto = await _userService.GetProfileOfCurrentUserAsync();

            // Assert
            profileDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void Should_Delete()
        {
            // Act
            Func<Task> func = async () => await _userService.DeleteAsync("test");

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void ModifyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            MyProfileModifyDto modifyDto = null;

            // Act
            Func<Task> func = async () => await _userService.ModifyProfileOfCurrentUserAsync(
               modifyDto);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public async Task Should_Get_Appointments_Of_Current_User_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Me.AppointmentList.Query>())
                .Returns(new Tuple<IEnumerable<Appointment>, int>(new List<Appointment> { AppointmentSeedData.RockingXMasRehearsal }, 1));
            var expectedDto = new MyAppointmentListDto { UserAppointments = UserAppointmentDtoTestData.PerformerUserAppointments, TotalRecordsCount = 1 };
            _mapper.Map<MyAppointmentListDto>(Arg.Any<Tuple<IEnumerable<Appointment>, int>>()).Returns(expectedDto);
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Performer);

            // Act
            MyAppointmentListDto userAppointmentListDto = await _userService.GetAppointmentsOfCurrentUserAsync(null, null);

            // Assert
            userAppointmentListDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task Should_Set_Appointment_Participation_Prediction_Async()
        {
            // Arrange
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Performer);
            _mapper.Map<AppointmentParticipations.SetPrediction.Command>(Arg.Any<SetMyProjectAppointmentPredictionDto>())
                .Returns(new AppointmentParticipations.SetPrediction.Command());

            // Act
            await _userService.SetAppointmentParticipationPredictionAsync(new SetMyProjectAppointmentPredictionDto());

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AppointmentParticipations.SetPrediction.Command>());
        }

        [Test]
        public async Task Should_Send_QrCode_Async()
        {
            // Arrange
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Performer);

            // Act
            await _userService.SendQrCodeAsync();

            // Assert
            await _mediator.Received(1).Send(Arg.Any<SendQRCode.Command>());
        }
    }
}

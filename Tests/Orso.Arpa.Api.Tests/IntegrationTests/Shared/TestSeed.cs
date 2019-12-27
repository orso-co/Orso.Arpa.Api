using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class TestSeed
    {
        internal static async Task SeedDataAsync(
            UserManager<User> userManager,
            IRepository repository,
            IUnitOfWork unitOfWork)
        {
            await SeedPersonsAsync(repository);
            await SeedUsersAsync(userManager);
            await SeedAppointmentsAsync(repository);
            await SeedVenuesAsync(repository);
            await SeedRoomsAsync(repository);
            await SeedProjectsAsync(repository);

            if (!await unitOfWork.CommitAsync())
            {
                throw new System.Exception("Problem seeding test data");
            }
        }

        private static async Task SeedPersonsAsync(IRepository repository)
        {
            foreach (Person person in PersonSeedData.Persons)
            {
                await repository.AddAsync(person);
            }
        }

        private static async Task SeedAppointmentsAsync(IRepository repository)
        {
            foreach (Appointment appointment in AppointmentSeedData.Appointments)
            {
                await repository.AddAsync(appointment);
            }
        }

        private static async Task SeedVenuesAsync(IRepository repository)
        {
            foreach (Venue venue in VenueSeedData.Venues)
            {
                await repository.AddAsync(venue);
            }
        }

        private static async Task SeedRoomsAsync(IRepository repository)
        {
            foreach (Room room in RoomSeedData.Rooms)
            {
                await repository.AddAsync(room);
            }
        }

        private static async Task SeedProjectsAsync(IRepository repository)
        {
            foreach (Project project in ProjectSeedData.Projects)
            {
                await repository.AddAsync(project);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                foreach (User user in UserSeedData.Users)
                {
                    await userManager.CreateAsync(user, UserSeedData.ValidPassword);
                }

                User orsianer = await userManager.FindByEmailAsync(UserSeedData.Orsianer.Email);
                await userManager.AddToRoleAsync(orsianer, RoleNames.Orsianer);

                User orsonaut = await userManager.FindByEmailAsync(UserSeedData.Orsonaut.Email);
                await userManager.AddToRoleAsync(orsonaut, RoleNames.Orsonaut);

                User admin = await userManager.FindByEmailAsync(UserSeedData.Orsoadmin.Email);
                await userManager.AddToRoleAsync(admin, RoleNames.Orsoadmin);
            }
        }
    }
}

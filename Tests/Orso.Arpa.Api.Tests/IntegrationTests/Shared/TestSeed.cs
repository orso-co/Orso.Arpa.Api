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
            SignInManager<User> signInManager,
            IArpaContext arpaContext)
        {
            await SeedPersonsAsync(arpaContext);
            await SeedUsersAsync(userManager, signInManager);
            await SeedAppointmentsAsync(arpaContext);
            await SeedVenuesAsync(arpaContext);
            await SeedRoomsAsync(arpaContext);
            await SeedProjectsAsync(arpaContext);
            await SeedMusicianProfilesAsync(arpaContext);
            await SeedProjectParticipationsAsync(arpaContext);
            await SeedAppointmentParticipationsAsync(arpaContext);

            if (!(await arpaContext.SaveChangesAsync(default) > 0))
            {
                throw new System.Exception("Problem seeding test data");
            }
        }

        private static async Task SeedPersonsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Persons.AddRangeAsync(PersonSeedData.Persons);
        }

        private static async Task SeedAppointmentsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Appointments.AddRangeAsync(AppointmentSeedData.Appointments);
        }

        private static async Task SeedVenuesAsync(IArpaContext arpaContext)
        {
            await arpaContext.Venues.AddRangeAsync(VenueSeedData.Venues);
        }

        private static async Task SeedRoomsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Rooms.AddRangeAsync(RoomSeedData.Rooms);
        }

        private static async Task SeedProjectsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Projects.AddRangeAsync(ProjectSeedData.Projects);
        }

        private static async Task SeedMusicianProfilesAsync(IArpaContext arpaContext)
        {
            await arpaContext.MusicianProfiles.AddRangeAsync(MusicianProfileSeedData.MusicianProfiles);
        }

        private static async Task SeedProjectParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.ProjectParticipations.AddRangeAsync(ProjectParticipationSeedData.ProjectParticipations);
        }

        private static async Task SeedAppointmentParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.AppointmentParticipations.AddRangeAsync(AppointmentParticipationSeedData.AppointmentParticipations);
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            if (!userManager.Users.Any())
            {
                foreach (User user in UserSeedData.Users)
                {
                    await userManager.CreateAsync(user, UserSeedData.ValidPassword);
                }

                User orsianer = await userManager.FindByEmailAsync(UserSeedData.Orsianer.Email);
                await userManager.AddToRoleAsync(orsianer, RoleNames.Performer);

                User orsonaut = await userManager.FindByEmailAsync(UserSeedData.Orsonaut.Email);
                await userManager.AddToRoleAsync(orsonaut, RoleNames.Staff);

                User admin = await userManager.FindByEmailAsync(UserSeedData.Orsoadmin.Email);
                await userManager.AddToRoleAsync(admin, RoleNames.Admin);

                User lockedOutUser = await userManager.FindByNameAsync(UserSeedData.LockedOutUser.UserName);
                for (int i = 0; i < 3; i++)
                {
                    await signInManager.CheckPasswordSignInAsync(lockedOutUser, "wrongPassword", true);
                }
            }
        }
    }
}

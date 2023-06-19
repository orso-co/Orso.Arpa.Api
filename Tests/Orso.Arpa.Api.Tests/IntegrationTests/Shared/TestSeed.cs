using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class TestSeed
    {
        internal static async Task SeedDataAsync(
            ArpaUserManager userManager,
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
            await SeedEducationsAsync(arpaContext);
            await SeedCurriculumVitaeReferencesAsync(arpaContext);
            await SeedMusicianProfileDeactivationsAsync(arpaContext);
            await SeedProjectParticipationsAsync(arpaContext);
            await SeedAppointmentParticipationsAsync(arpaContext);
            await SeedUrlAsync(arpaContext);
            await SeedAuditLogAsync(arpaContext);
            await SeedNewsAsync(arpaContext);

            if (await arpaContext.SaveChangesAsync(default) <= 0)
            {
                throw new System.Exception("Problem seeding test data");
            }
        }

        private static async Task SeedPersonsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Persons.AddRangeAsync(PersonTestSeedData.Persons);
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

        private static async Task SeedEducationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Educations.AddRangeAsync(EducationSeedData.Educations);
        }

        private static async Task SeedCurriculumVitaeReferencesAsync(IArpaContext arpaContext)
        {
            await arpaContext.CurriculumVitaeReferences.AddRangeAsync(CurriculumVitaeReferenceSeedData.CurriculumVitaeReferences);
        }

        private static async Task SeedMusicianProfileDeactivationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.MusicianProfileDeactivations.AddRangeAsync(MusicianProfileDeactivationSeedData.MusicianProfileDeactivations);
        }

        private static async Task SeedProjectParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.ProjectParticipations.AddRangeAsync(ProjectParticipationSeedData.ProjectParticipations);
        }

        private static async Task SeedAppointmentParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.AppointmentParticipations.AddRangeAsync(AppointmentParticipationSeedData.AppointmentParticipations);
        }

        private static async Task SeedUrlAsync(IArpaContext arpaContext)
        {
            await arpaContext.Urls.AddRangeAsync(UrlSeedData.Urls);
        }

        private static async Task SeedAuditLogAsync(IArpaContext arpaContext)
        {
            await arpaContext.AuditLogs.AddRangeAsync(AuditLogSeedData.AuditLogs);
        }
        private static async Task SeedNewsAsync(IArpaContext arpaContext)
        {
            await arpaContext.News.AddRangeAsync(NewsSeedData.News);
        }

        private static async Task SeedUsersAsync(ArpaUserManager userManager, SignInManager<User> signInManager)
        {
            foreach (User user in UserTestSeedData.Users)
            {
                if ((await userManager.FindByNameAsync(user.UserName)) is null)
                {
                    await userManager.CreateAsync(user, UserSeedData.ValidPassword);
                }
            }

            User performer = await userManager.FindByEmailAsync(UserTestSeedData.Performer.Email);
            if (!(await userManager.IsInRoleAsync(performer, RoleNames.Performer)))
            {
                await userManager.AddToRoleAsync(performer, RoleNames.Performer);
            }

            User staff = await userManager.FindByEmailAsync(UserTestSeedData.Staff.Email);
            if (!(await userManager.IsInRoleAsync(staff, RoleNames.Staff)))
            {
                await userManager.AddToRoleAsync(staff, RoleNames.Staff);
            }

            User lockedOutUser = await userManager.FindByNameAsync(UserTestSeedData.LockedOutUser.UserName);
            for (int i = 0; i < 3; i++)
            {
                await signInManager.CheckPasswordSignInAsync(lockedOutUser, "wrongPassword", true);
            }
        }
    }
}

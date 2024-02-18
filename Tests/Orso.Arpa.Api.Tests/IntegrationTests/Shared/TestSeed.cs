using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.NewsDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Domain.VenueDomain.Model;
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
            await SeedRoomSectionsAsync(arpaContext);
            await SeedRoomEquipmentsAsync(arpaContext);
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
            await arpaContext.Set<Person>().AddRangeAsync(PersonTestSeedData.Persons);
        }

        private static async Task SeedAppointmentsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Appointment>().AddRangeAsync(AppointmentSeedData.Appointments);
        }

        private static async Task SeedVenuesAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Venue>().AddRangeAsync(VenueSeedData.Venues);
        }

        private static async Task SeedRoomsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Room>().AddRangeAsync(RoomSeedData.Rooms);
        }

        private static async Task SeedRoomSectionsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<RoomSection>().AddRangeAsync(RoomSectionSeedData.RoomSections);
        }

        private static async Task SeedRoomEquipmentsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<RoomEquipment>().AddRangeAsync(RoomEquipmentSeedData.RoomEquipments);
        }

        private static async Task SeedProjectsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Project>().AddRangeAsync(ProjectSeedData.Projects);
        }

        private static async Task SeedMusicianProfilesAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<MusicianProfile>().AddRangeAsync(MusicianProfileSeedData.MusicianProfiles);
        }

        private static async Task SeedEducationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Education>().AddRangeAsync(EducationSeedData.Educations);
        }

        private static async Task SeedCurriculumVitaeReferencesAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<CurriculumVitaeReference>().AddRangeAsync(CurriculumVitaeReferenceSeedData.CurriculumVitaeReferences);
        }

        private static async Task SeedMusicianProfileDeactivationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<MusicianProfileDeactivation>().AddRangeAsync(MusicianProfileDeactivationSeedData.MusicianProfileDeactivations);
        }

        private static async Task SeedProjectParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<ProjectParticipation>().AddRangeAsync(ProjectParticipationSeedData.ProjectParticipations);
        }

        private static async Task SeedAppointmentParticipationsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<AppointmentParticipation>().AddRangeAsync(AppointmentParticipationSeedData.AppointmentParticipations);
        }

        private static async Task SeedUrlAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<Url>().AddRangeAsync(UrlSeedData.Urls);
        }

        private static async Task SeedAuditLogAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<AuditLog>().AddRangeAsync(AuditLogSeedData.AuditLogs);
        }
        private static async Task SeedNewsAsync(IArpaContext arpaContext)
        {
            await arpaContext.Set<News>().AddRangeAsync(NewsSeedData.News);
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
            if (!await userManager.IsInRoleAsync(performer, RoleNames.Performer))
            {
                await userManager.AddToRoleAsync(performer, RoleNames.Performer);
            }

            User staff = await userManager.FindByEmailAsync(UserTestSeedData.Staff.Email);
            if (!await userManager.IsInRoleAsync(staff, RoleNames.Staff))
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

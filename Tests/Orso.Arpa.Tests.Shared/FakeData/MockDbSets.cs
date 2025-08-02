using System.Linq;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class MockDbSets
    {
        public static DbSet<Appointment> Appointments => AppointmentSeedData.Appointments.BuildMockDbSet();
        public static DbSet<Person> Persons => PersonTestSeedData.Persons.BuildMockDbSet();
        public static DbSet<Project> Projects => ProjectSeedData.Projects.BuildMockDbSet();
        public static DbSet<Url> Urls => UrlSeedData.Urls.BuildMockDbSet();
        public static DbSet<Room> Rooms => RoomSeedData.Rooms.BuildMockDbSet();
        public static DbSet<SelectValueCategory> SelectValueCategories => FakeSelectValueCategories.SelectValueCategories.BuildMockDbSet();
        public static DbSet<SelectValue> SelectValues => SelectValueSeedData.SelectValues.BuildMockDbSet();
        public static DbSet<SelectValueMapping> SelectValueMappings => SelectValueMappingSeedData.SelectValueMappings.BuildMockDbSet();
        public static DbSet<ProjectAppointment> ProjectAppointments => AppointmentSeedData.RockingXMasRehearsal.ProjectAppointments.BuildMockDbSet();
        public static DbSet<AppointmentRoom> AppointmentRooms => AppointmentSeedData.AfterShowParty.AppointmentRooms.BuildMockDbSet();
        public static DbSet<SectionAppointment> SectionAppointments => AppointmentSeedData.AfterShowParty.SectionAppointments.BuildMockDbSet();
        public static DbSet<Section> Sections => SectionSeedData.Sections.BuildMockDbSet();
        public static DbSet<Venue> Venues => VenueSeedData.Venues.BuildMockDbSet();
        public static DbSet<Region> Regions => RegionSeedData.Regions.BuildMockDbSet();
        public static DbSet<MusicianProfile> MusicianProfiles => MusicianProfileSeedData.MusicianProfiles.BuildMockDbSet();
        public static readonly DbSet<ProjectParticipation> ProjectParticipations = FakeProjectParticipations.ProjectParticipations.BuildMockDbSet();
    }
}

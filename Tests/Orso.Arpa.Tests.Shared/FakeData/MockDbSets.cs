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
        public static DbSet<Appointment> Appointments => AppointmentSeedData.Appointments.AsQueryable().BuildMockDbSet();
        public static DbSet<Person> Persons => PersonTestSeedData.Persons.AsQueryable().BuildMockDbSet();
        public static DbSet<Project> Projects => ProjectSeedData.Projects.AsQueryable().BuildMockDbSet();
        public static DbSet<Url> Urls => UrlSeedData.Urls.AsQueryable().BuildMockDbSet();
        public static DbSet<Room> Rooms => RoomSeedData.Rooms.AsQueryable().BuildMockDbSet();
        public static DbSet<SelectValueCategory> SelectValueCategories => FakeSelectValueCategories.SelectValueCategories.AsQueryable().BuildMockDbSet();
        public static DbSet<SelectValue> SelectValues => SelectValueSeedData.SelectValues.AsQueryable().BuildMockDbSet();
        public static DbSet<SelectValueMapping> SelectValueMappings => SelectValueMappingSeedData.SelectValueMappings.AsQueryable().BuildMockDbSet();
        public static DbSet<ProjectAppointment> ProjectAppointments => AppointmentSeedData.RockingXMasRehearsal.ProjectAppointments.AsQueryable().BuildMockDbSet();
        public static DbSet<AppointmentRoom> AppointmentRooms => AppointmentSeedData.AfterShowParty.AppointmentRooms.AsQueryable().BuildMockDbSet();
        public static DbSet<SectionAppointment> SectionAppointments => AppointmentSeedData.AfterShowParty.SectionAppointments.AsQueryable().BuildMockDbSet();
        public static DbSet<Section> Sections => SectionSeedData.Sections.AsQueryable().BuildMockDbSet();
        public static DbSet<Venue> Venues => VenueSeedData.Venues.AsQueryable().BuildMockDbSet();
        public static DbSet<Region> Regions => RegionSeedData.Regions.AsQueryable().BuildMockDbSet();
        public static DbSet<MusicianProfile> MusicianProfiles => MusicianProfileSeedData.MusicianProfiles.AsQueryable().BuildMockDbSet();
        public static readonly DbSet<ProjectParticipation> ProjectParticipations = FakeProjectParticipations.ProjectParticipations.AsQueryable().BuildMockDbSet();
    }
}

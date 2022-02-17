using System;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Persistence.EntityConfigurations;

namespace Orso.Arpa.Persistence.GraphQL
{
    public class GraphQLContext : IdentityDbContext<User, Role, Guid>
    {
        public GraphQLContext(DbContextOptions<GraphQLContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        public DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        public DbSet<Audition> Auditions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<CurriculumVitaeReference> CurriculumVitaeReferences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<MusicianProfile> MusicianProfiles { get; set; }
        public DbSet<MusicianProfileDeactivation> MusicianProfileDeactivations { get; set; }
        public DbSet<MusicianProfileDocument> MusicianProfileDocuments { get; set; }
        public DbSet<MusicianProfilePositionInner> MusicianProfilePositionsInner { get; set; }
        public DbSet<MusicianProfilePositionTeam> MusicianProfilePositionsTeam { get; set; }
        public DbSet<MusicianProfileSection> MusicianProfileSections { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonSection> PersonSections { get; set; }
        public DbSet<PreferredGenre> PreferredGenres { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAppointment> ProjectAppointments { get; set; }
        public DbSet<ProjectParticipation> ProjectParticipations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionPreference> RegionPreferences { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionAppointment> SectionAppointments { get; set; }
        public DbSet<SelectValue> SelectValues { get; set; }
        public DbSet<SelectValueCategory> SelectValueCategories { get; set; }
        public DbSet<SelectValueMapping> SelectValueMappings { get; set; }
        public DbSet<SelectValueSection> SelectValueSections { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlRole> UrlRoles { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (Type type in ArpaContextUtility.GetEntityTypes())

            {
                MethodInfo method = ArpaContextUtility.SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}

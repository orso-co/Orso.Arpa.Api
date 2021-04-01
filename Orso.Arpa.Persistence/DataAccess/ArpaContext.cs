using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Configurations;

namespace Orso.Arpa.Persistence.DataAccess
{
    public class ArpaContext : IdentityDbContext<User, Role, Guid>, IArpaContext
    {
        private readonly ITokenAccessor _tokenAccessor;

        public ArpaContext(
            DbContextOptions options,
            ITokenAccessor tokenAccessor) : base(options)
        {
            _tokenAccessor = tokenAccessor;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        public DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<MusicianProfile> MusicianProfiles { get; set; }
        public DbSet<MusicianProfileCredential> MusicianProfileCredentials { get; set; }
        public DbSet<MusicianProfileEducation> MusicianProfileEducations { get; set; }
        public DbSet<MusicianProfileSection> MusicianProfileSections { get; set; }
        public DbSet<MusicianProfileSelectValueMapping> MusicianProfileSelectValueMappings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<PersonSection> PersonSections { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAppointment> ProjectAppointments { get; set; }
        public DbSet<ProjectParticipation> ProjectParticipations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionAppointment> SectionAppointments { get; set; }
        public DbSet<SelectValue> SelectValues { get; set; }
        public DbSet<SelectValueCategory> SelectValueCategories { get; set; }
        public DbSet<SelectValueMapping> SelectValueMappings { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlRole> UrlRoles { get; set; }


        public DbSet<Venue> Venues { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUserDisplayName = _tokenAccessor.DisplayName;

            foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Create(currentUserDisplayName);
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modify(currentUserDisplayName);
                        break;

                    case EntityState.Deleted:
                        await DeleteWithNavigationsAsync(currentUserDisplayName, entry, cancellationToken);
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task DeleteWithNavigationsAsync(string currentUserDisplayName, EntityEntry entry, CancellationToken cancellationToken)
        {
            entry.State = EntityState.Modified;
            (entry.Entity as BaseEntity)?.Delete(currentUserDisplayName);

            foreach (IProperty property in entry.CurrentValues.Properties)
            {
                if (property.IsColumnNullable() && property.IsForeignKey())
                {
                    entry.CurrentValues[property] = null;
                }
            }
            foreach (NavigationEntry navigationEntry in entry.Navigations)
            {
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    if (!collectionEntry.IsLoaded)
                    {
                        await collectionEntry.LoadAsync(cancellationToken);
                    }
                    foreach (var dependentEntryObject in collectionEntry.CurrentValue)
                    {
                        EntityEntry dependentEntry = Entry(dependentEntryObject);
                        if (dependentEntry.Entity.GetType().IsSubclassOf(typeof(BaseEntity)))
                        {
                            await DeleteWithNavigationsAsync(currentUserDisplayName, dependentEntry, cancellationToken);
                        }
                    }
                }
            }
        }

        public void ClearChangeTracker()
        {
            ChangeTracker.Clear();
        }
    }
}

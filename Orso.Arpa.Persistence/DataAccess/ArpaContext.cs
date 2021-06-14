using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Views;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.EntityConfigurations;

namespace Orso.Arpa.Persistence.DataAccess
{
    public class ArpaContext : IdentityDbContext<User, Role, Guid>, IArpaContext
    {
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        public delegate Task CallBack<T>() where T : BaseEntity;
        private readonly CallBack<Localization> _translationCallBack;

        public ArpaContext(
            DbContextOptions options,
            ITokenAccessor tokenAccessor,
            IDateTimeProvider dateTimeProvider,
            CallBack<Localization> translationCallBack) : base(options)
        {
            _tokenAccessor = tokenAccessor;
            _dateTimeProvider = dateTimeProvider;
            _translationCallBack = translationCallBack;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        public DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        public DbSet<Audition> Auditions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AvailableDocument> AvailableDocuments { get; set; }
        public DbSet<CurriculumVitaeReference> CurriculumVitaeReference { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<MusicianProfile> MusicianProfiles { get; set; }
        public DbSet<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; set; }
        public DbSet<MusicianProfileEducation> MusicianProfileEducations { get; set; }
        public DbSet<MusicianProfilePositionInner> MusicianProfilePositionsInner { get; set; }
        public DbSet<MusicianProfilePositionTeam> MusicianProfilePositionsTeam { get; set; }
        public DbSet<MusicianProfileSection> MusicianProfileSections { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<PersonSection> PersonSections { get; set; }
        public DbSet<PreferredGenre> PreferredGenres { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAppointment> ProjectAppointments { get; set; }
        public DbSet<ProjectParticipation> ProjectParticipations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionPreferencePerformance> RegionPreferencesPerformance { get; set; }
        public DbSet<RegionPreferenceRehearsal> RegionPreferencesRehearsal { get; set; }
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
        public DbSet<Localization> Localizations { get; set; }

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

            builder.Entity<Url>()
                .HasQueryFilter(url => !url.Deleted
                    && (url.UrlRoles.Count == 0
                    || url.UrlRoles.Select(r => r.Role.Name).Any(name => _tokenAccessor.UserRoles.Contains(name))));

            builder.HasDbFunction(typeof(ArpaContext).GetMethod(nameof(GetAppointmentIdsForPerson), new[] { typeof(Guid) }))
                .HasName("fn_appointments_for_person");

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
                        entry.Entity.Create(currentUserDisplayName, _dateTimeProvider.GetUtcNow());
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modify(currentUserDisplayName, _dateTimeProvider.GetUtcNow());
                        break;

                    case EntityState.Deleted:
                        await DeleteWithNavigationsAsync(currentUserDisplayName, entry, cancellationToken);
                        break;
                }
            }

            SaveAuditTrail(currentUserDisplayName);

            int task = await base.SaveChangesAsync(cancellationToken);

            if(!ChangeTracker.Entries<Localization>().IsNullOrEmpty())
                await _translationCallBack();

            return task;
        }

        private async Task DeleteWithNavigationsAsync(string currentUserDisplayName, EntityEntry entry, CancellationToken cancellationToken)
        {
            entry.State = EntityState.Modified;
            (entry.Entity as BaseEntity)?.Delete(currentUserDisplayName, _dateTimeProvider.GetUtcNow());

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

        private void SaveAuditTrail(string currentUserDisplayName)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditLog>();
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditLog()
                {
                    TableName = entry.Entity.GetType().Name,
                    CreatedBy = currentUserDisplayName,
                    CreatedAt = _dateTimeProvider.GetUtcNow()
                };

                auditEntries.Add(auditEntry);

                foreach (PropertyEntry property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        Dictionary<string, Guid> foo = JsonSerializer.Deserialize<Dictionary<string, Guid>>(auditEntry.KeyValues, null);
                        foo[propertyName] = (Guid)property.CurrentValue;
                        auditEntry.KeyValues = JsonSerializer.Serialize(foo);
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.Type = AuditLogType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.Type = AuditLogType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.Type = AuditLogType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (AuditLog auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry);
            }
        }

        public void ClearChangeTracker()
        {
            ChangeTracker.Clear();
        }

        public async Task<bool> EntityExistsAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return await Set<TEntity>()
                .AsQueryable()
                .AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<bool> EntityExistsAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await Set<TEntity>()
                .AsQueryable()
                .AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Method body for database function
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        /// <see cref="https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping"/>
        public IQueryable<AppointmentForPerson> GetAppointmentIdsForPerson(Guid personId) => FromExpression(() => GetAppointmentIdsForPerson(personId));
    }
}

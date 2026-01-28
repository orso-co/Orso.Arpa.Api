using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Orso.Arpa.Domain._General.Interfaces;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.NewsDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.EntityConfigurations;

namespace Orso.Arpa.Persistence.DataAccess
{
    public class ArpaContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IArpaContext
    {
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        public delegate Task CallBack<T>() where T : BaseEntity;
        private readonly CallBack<Localization> _translationCallBack;

        public ArpaContext(
            DbContextOptions<ArpaContext> options,
            ITokenAccessor tokenAccessor,
            IDateTimeProvider dateTimeProvider,
            CallBack<Localization> translationCallBack) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _tokenAccessor = tokenAccessor;
            _dateTimeProvider = dateTimeProvider;
            _translationCallBack = translationCallBack;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        public DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        public DbSet<AppointmentSetlistPiece> AppointmentSetlistPieces { get; set; }
        public DbSet<Audition> Auditions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubMembershipType> ClubMembershipTypes { get; set; }
        public DbSet<ClubMembershipSubType> ClubMembershipSubTypes { get; set; }
        public DbSet<ClubMembershipProfile> ClubMembershipProfiles { get; set; }
        public DbSet<CurriculumVitaeReference> CurriculumVitaeReferences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<MusicianProfile> MusicianProfiles { get; set; }
        public DbSet<MusicianProfileDeactivation> MusicianProfileDeactivations { get; set; }
        public DbSet<MusicianProfileDocument> MusicianProfileDocuments { get; set; }
        public DbSet<MusicianProfilePositionInner> MusicianProfilePositionsInner { get; set; }
        public DbSet<MusicianProfilePositionTeam> MusicianProfilePositionsTeam { get; set; }
        public DbSet<MusicianProfileSection> MusicianProfileSections { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonMembership> PersonMemberships { get; set; }
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

        // Music Library
        public DbSet<MusicPiece> MusicPieces { get; set; }
        public DbSet<MusicPiecePart> MusicPieceParts { get; set; }
        public DbSet<MusicPieceFile> MusicPieceFiles { get; set; }
        public DbSet<MusicPieceFileRole> MusicPieceFileRoles { get; set; }
        public DbSet<MusicPieceFileSection> MusicPieceFileSections { get; set; }
        public DbSet<Setlist> Setlists { get; set; }
        public DbSet<SetlistPiece> SetlistPieces { get; set; }

        // Stage Setups (Aufstellungen)
        public DbSet<StageSetup> StageSetups { get; set; }
        public DbSet<StageSetupPosition> StageSetupPositions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (Type type in ArpaContextUtility.GetEntityTypes())

            {
                MethodInfo method = ArpaContextUtility.SetGlobalQueryMethod.MakeGenericMethod(type);
                _ = method.Invoke(this, [builder]);
            }

            _ = builder.Entity<Url>()
                .HasQueryFilter(url => !url.Deleted
                    && (url.UrlRoles.Count == 0
                    || _tokenAccessor.GetUserRoles().Contains(RoleNames.Staff)
                    || url.UrlRoles.Select(r => r.Role.Name).Any(name => _tokenAccessor.GetUserRoles().Contains(name))));

            _ = builder
                .HasDbFunction(typeof(ArpaContext)
                .GetMethod(nameof(GetAppointmentIdsForPerson), [typeof(Guid)]))
                .HasName("fn_active_appointments_for_person");
            _ = builder
                .HasDbFunction(typeof(ArpaContext)
                .GetMethod(nameof(GetMusicianProfilesForAppointment), [typeof(Guid)]))
                .HasName("fn_mupro_for_appointments");
            _ = builder
                .HasDbFunction(typeof(ArpaContext)
                .GetMethod(nameof(GetPersonsForAppointment), [typeof(Guid)]))
                .HasName("fn_persons_for_appointment");

            base.OnModelCreating(builder);
            _ = builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUserDisplayName = _tokenAccessor.DisplayName;
            DateTime now = _dateTimeProvider.GetUtcNow();

            foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Create(currentUserDisplayName, now);
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modify(currentUserDisplayName, now);
                        break;

                    case EntityState.Deleted:
                        await DeleteWithNavigationsAsync(currentUserDisplayName, entry, cancellationToken);
                        break;
                }
            }

            SaveAuditTrail(currentUserDisplayName);

            int saveResult = await base.SaveChangesAsync(cancellationToken);

            if (!ChangeTracker.Entries<Localization>().Any())
            {
                await _translationCallBack();
            }

            return saveResult;
        }

        private async Task DeleteWithNavigationsAsync(string currentUserDisplayName, EntityEntry entry, CancellationToken cancellationToken)
        {
            Type entityType = entry.Entity.GetType();

            DeleteEntity(currentUserDisplayName, entry, entityType);

            await DeleteNavigationEntriesAsync(currentUserDisplayName, entry, entityType, cancellationToken);

            SetNullableForeignKeysToNull(entry);
        }

        private static void SetNullableForeignKeysToNull(EntityEntry entry)
        {
            foreach (IProperty property in from IProperty property in entry.CurrentValues.Properties
                                     where property.IsColumnNullable() && property.IsForeignKey()
                                     select property)
            {
                entry.CurrentValues[property] = null;
            }

        }

        private async Task DeleteNavigationEntriesAsync(string currentUserDisplayName, EntityEntry entry, Type entityType, CancellationToken cancellationToken)
        {
            foreach (NavigationEntry navigationEntry in entry.Navigations)
            {
                var shouldBeDeleted = entityType
                    .GetProperty(navigationEntry.Metadata.Name)?
                    .GetCustomAttribute<CascadingSoftDeleteAttribute>() != null;

                if (!shouldBeDeleted)
                {
                    continue;
                }
                if (!navigationEntry.IsLoaded)
                {
                    await navigationEntry.LoadAsync(cancellationToken);
                }
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (var dependentEntryObject in collectionEntry.CurrentValue)
                    {
                        await DeleteNavigationEntry(currentUserDisplayName, dependentEntryObject, cancellationToken);
                    }
                }
                else if (navigationEntry is ReferenceEntry referenceEntry && referenceEntry.CurrentValue != null)
                {
                    await DeleteNavigationEntry(currentUserDisplayName, referenceEntry.CurrentValue, cancellationToken);
                }
            }
        }

        private void DeleteEntity(string currentUserDisplayName, EntityEntry entry, Type entityType)
        {
            if (entityType.GetCustomAttribute<HardDeleteAttribute>() == null)
            {
                entry.State = EntityState.Modified;
                (entry.Entity as BaseEntity)?.Delete(currentUserDisplayName, _dateTimeProvider.GetUtcNow());
            }
        }

        private async Task DeleteNavigationEntry(string currentUserDisplayName, object navigationEntryCurrentValue, CancellationToken cancellationToken)
        {
            EntityEntry dependentEntry = Entry(navigationEntryCurrentValue);
            Type typeOfDependentEntry = dependentEntry.Entity.GetType();
            if (typeOfDependentEntry.IsSubclassOf(typeof(BaseEntity)))
            {
                await DeleteWithNavigationsAsync(currentUserDisplayName, dependentEntry, cancellationToken);
            }
        }

        private const string SHADOW_VALUE = "**********";
        private void SaveAuditTrail(string currentUserDisplayName)
        {
            ChangeTracker.DetectChanges();
            var auditLogs = new List<AuditLog>();

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                Type entityType = entry.Entity.GetType();

                if (entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged
                    || entityType.GetCustomAttribute<AuditLogIgnoreAttribute>() != null)
                {
                    continue;
                }

                auditLogs.Add(createAuditTrailEntry(currentUserDisplayName, entry, entityType));
            }
            // Das kann nicht direkt in der oberen foreach Schleife erfolgen, weil sonst der Changetracker während der Iteration verändert wird und eine Exception schmeißt
            AuditLogs.AddRange(auditLogs);
        }

        private AuditLog createAuditTrailEntry(string currentUserDisplayName, EntityEntry entry, Type entityType)
        {
            var auditEntry = new AuditLog()
            {
                TableName = entityType.Name,
                CreatedBy = currentUserDisplayName,
                CreatedAt = _dateTimeProvider.GetUtcNow()
            };

            foreach (PropertyEntry property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                if (property.Metadata.IsPrimaryKey())
                {
                    Dictionary<string, Guid> keyValues = JsonSerializer.Deserialize<Dictionary<string, Guid>>(auditEntry.KeyValues, (JsonSerializerOptions)null);
                    keyValues[propertyName] = (Guid)property.CurrentValue;
                    auditEntry.KeyValues = JsonSerializer.Serialize(keyValues);
                    continue;
                }

                var isShadowed =
                    property.Metadata.ClrType == typeof(string)
                    && property.Metadata.PropertyInfo?.GetCustomAttribute<AuditLogIgnoreAttribute>() != null;

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.Type = AuditLogType.Create;
                        auditEntry.NewValues[propertyName] = isShadowed ? SHADOW_VALUE : property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.Type = AuditLogType.Delete;
                        auditEntry.OldValues[propertyName] = isShadowed ? SHADOW_VALUE : property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.Type = AuditLogType.Update;
                            auditEntry.OldValues[propertyName] = isShadowed ? SHADOW_VALUE : property.OriginalValue;
                            auditEntry.NewValues[propertyName] = isShadowed ? SHADOW_VALUE : property.CurrentValue;
                        }
                        break;
                }
            }
            return auditEntry;
        }

        public void ClearChangeTracker()
        {
            ChangeTracker.Clear();
        }

        public bool EntityExists<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return Set<TEntity>()
                .AsQueryable()
                .Any(entity => entity.Id == id);
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

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return await FindAsync<TEntity>([id], cancellationToken);
        }

        public async Task<TVersionedEntity> GetCurrentAsync<TVersionedEntity>(CancellationToken cancellationToken) where TVersionedEntity : BaseEntity, IVersionedEntity {
            return await Set<TVersionedEntity>()
                .AsQueryable()
                .Where(entity => entity.ValidFrom <= _dateTimeProvider.GetUtcNow())
                .OrderByDescending(entity => entity.ValidFrom)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// CLR method for database function fn_appointments_for_person
        /// </summary>
        /// <param name="personId"></param>
        /// <see cref="https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping"/>
        public IQueryable<SqlFunctionIdResult> GetAppointmentIdsForPerson(Guid personId) => FromExpression(() => GetAppointmentIdsForPerson(personId));

        /// <summary>
        /// CLR method for database function fn_mupro_for_appointments
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <see cref="https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping"/>
        public IQueryable<SqlFunctionIdResult> GetMusicianProfilesForAppointment(Guid appointmentId) => FromExpression(() => GetMusicianProfilesForAppointment(appointmentId));

        /// <summary>
        /// CLR method for database function fn_person_for_appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <see cref="https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping"/>
        public IQueryable<SqlFunctionIdResult> GetPersonsForAppointment(Guid appointmentId) => FromExpression(() => GetPersonsForAppointment(appointmentId));

        /// <summary>
        /// Calls the fn_is_person_eligible_for_appointment function
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        /// <remarks>Bei DB Functions mit skalarem Rückgabewert funktioniert das CLR Mapping bei EF Core leider (noch) nicht</remarks>
        public bool IsPersonEligibleForAppointment(Guid personId, Guid appointmentId)
        {
            Database.OpenConnection();
            using DbCommand command = Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT public.fn_is_person_eligible_for_appointment('{personId}', '{appointmentId}')";
            return (bool)command.ExecuteScalar();
        }

        public async Task<int> ExecuteSqlAsync(string sqlStatement)
        {
            return await Database.ExecuteSqlRawAsync(sqlStatement);
        }

        /// <summary>
        /// Global search across Persons, Appointments, Projects, and News using pg_trgm
        /// </summary>
        public async Task<List<GlobalSearchResult>> GlobalSearchAsync(string searchQuery, int maxResults = 50)
        {
            if (string.IsNullOrWhiteSpace(searchQuery) || searchQuery.Length < 2)
            {
                return new List<GlobalSearchResult>();
            }

            var results = new List<GlobalSearchResult>();

            await Database.OpenConnectionAsync();
            try
            {
                await using DbCommand command = Database.GetDbConnection().CreateCommand();
                command.CommandText = "SELECT entity_type, entity_id, display_name, additional_info, relevance FROM fn_global_search(@query, @maxResults)";

                var queryParam = command.CreateParameter();
                queryParam.ParameterName = "@query";
                queryParam.Value = searchQuery;
                command.Parameters.Add(queryParam);

                var maxResultsParam = command.CreateParameter();
                maxResultsParam.ParameterName = "@maxResults";
                maxResultsParam.Value = maxResults;
                command.Parameters.Add(maxResultsParam);

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    results.Add(new GlobalSearchResult
                    {
                        EntityType = reader.GetString(0),
                        EntityId = reader.GetGuid(1),
                        DisplayName = reader.GetString(2),
                        AdditionalInfo = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Relevance = reader.GetFloat(4)
                    });
                }
            }
            finally
            {
                await Database.CloseConnectionAsync();
            }

            return results;
        }
    }
}

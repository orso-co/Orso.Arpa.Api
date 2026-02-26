using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.NewsDomain.Model;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Domain.ChatDomain.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.SurveyDomain.Model;
using Orso.Arpa.Domain.TodoDomain.Model;
using Orso.Arpa.Domain.MediathekDomain.Model;
using Orso.Arpa.Domain.TicketDomain.Model;
using Orso.Arpa.Domain.ActivityLogDomain.Model;
using Orso.Arpa.Domain.FinanceDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IArpaContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        DbSet<AppointmentSetlistPiece> AppointmentSetlistPieces { get; set; }
        DbSet<MusicianProfile> MusicianProfiles { get; set; }
        DbSet<Education> Educations { get; set; }
        DbSet<CurriculumVitaeReference> CurriculumVitaeReferences { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Url> Urls { get; set; }
        DbSet<UrlRole> UrlRoles { get; set; }
        DbSet<ProjectAppointment> ProjectAppointments { get; set; }
        DbSet<ProjectParticipation> ProjectParticipations { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<SectionDomain.Model.Section> Sections { get; set; }
        DbSet<SectionAppointment> SectionAppointments { get; set; }
        DbSet<SelectValueCategory> SelectValueCategories { get; set; }
        DbSet<SelectValue> SelectValues { get; set; }
        DbSet<SelectValueMapping> SelectValueMappings { get; set; }
        DbSet<Venue> Venues { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }
        DbSet<Localization> Localizations { get; set; }
        DbSet<News> News { get; set; }
        DbSet<NewsReadStatus> NewsReadStatuses { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<MusicianProfileDocument> MusicianProfileDocuments { get; set; }
        DbSet<MusicianProfileDeactivation> MusicianProfileDeactivations { get; set; }

        // Music Library
        DbSet<MusicPiece> MusicPieces { get; set; }
        DbSet<MusicPiecePart> MusicPieceParts { get; set; }
        DbSet<MusicPieceFile> MusicPieceFiles { get; set; }
        DbSet<MusicPieceFileRole> MusicPieceFileRoles { get; set; }
        DbSet<MusicPieceFileSection> MusicPieceFileSections { get; set; }
        DbSet<MusicPieceUrl> MusicPieceUrls { get; set; }
        DbSet<MusicPieceTodo> MusicPieceTodos { get; set; }
        DbSet<Setlist> Setlists { get; set; }
        DbSet<SetlistPiece> SetlistPieces { get; set; }

        // Organizations
        DbSet<Organization> Organizations { get; set; }
        DbSet<PersonOrganization> PersonOrganizations { get; set; }
        DbSet<OrganizationRelationship> OrganizationRelationships { get; set; }

        // Chat
        DbSet<ChatRoom> ChatRooms { get; set; }
        DbSet<ChatRoomMember> ChatRoomMembers { get; set; }
        DbSet<ChatRoomEntityLink> ChatRoomEntityLinks { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }
        DbSet<ChatMessageAttachment> ChatMessageAttachments { get; set; }
        DbSet<MessageReaction> MessageReactions { get; set; }
        DbSet<MessageReadReceipt> MessageReadReceipts { get; set; }
        DbSet<ChatFolder> ChatFolders { get; set; }
        DbSet<ChatFolderRoomAssignment> ChatFolderRoomAssignments { get; set; }
        DbSet<ChatLiveLocationShare> ChatLiveLocationShares { get; set; }

        // Push Notifications
        DbSet<PushSubscription> PushSubscriptions { get; set; }
        DbSet<NotificationPreference> NotificationPreferences { get; set; }
        DbSet<UserDashboardLayout> UserDashboardLayouts { get; set; }
        DbSet<UserSettings> UserSettings { get; set; }

        // Email Campaigns
        DbSet<EmailTemplate> EmailTemplates { get; set; }
        DbSet<EmailCampaign> EmailCampaigns { get; set; }
        DbSet<EmailCampaignRecipient> EmailCampaignRecipients { get; set; }
        DbSet<EmailCampaignAttachment> EmailCampaignAttachments { get; set; }
        DbSet<EmailUnsubscription> EmailUnsubscriptions { get; set; }

        // Todos
        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<TodoComment> TodoComments { get; set; }
        DbSet<TodoDependency> TodoDependencies { get; set; }

        // Surveys
        DbSet<Survey> Surveys { get; set; }
        DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        DbSet<SurveyAnswerOption> SurveyAnswerOptions { get; set; }
        DbSet<SurveyUserResponse> SurveyUserResponses { get; set; }
        DbSet<SurveyAnswer> SurveyAnswers { get; set; }

        // Instrumentations
        DbSet<Instrumentation> Instrumentations { get; set; }
        DbSet<InstrumentationPosition> InstrumentationPositions { get; set; }
        DbSet<InstrumentationPositionDoubling> InstrumentationPositionDoublings { get; set; }

        // Mediathek
        DbSet<MediathekAccess> MediathekAccesses { get; set; }
        DbSet<MediathekAccessRequest> MediathekAccessRequests { get; set; }

        // Tickets
        DbSet<Ticket> Tickets { get; set; }
        DbSet<TicketMessage> TicketMessages { get; set; }
        DbSet<TicketAttachment> TicketAttachments { get; set; }
        DbSet<TicketLink> TicketLinks { get; set; }
        DbSet<TicketVote> TicketVotes { get; set; }
        DbSet<TicketReaction> TicketReactions { get; set; }
        DbSet<TicketReadStatus> TicketReadStatuses { get; set; }

        // Calendar Feed
        DbSet<CalendarFeedToken> CalendarFeedTokens { get; set; }

        // Activity Logs
        DbSet<ActivityLog> ActivityLogs { get; set; }

        // Finance
        DbSet<OrganizationBankAccount> OrganizationBankAccounts { get; set; }
        DbSet<BankAccountBalanceSnapshot> BankAccountBalanceSnapshots { get; set; }
        DbSet<PendingTanRequest> PendingTanRequests { get; set; }
        DbSet<FinanceClubAccess> FinanceClubAccesses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void ClearChangeTracker();
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> EntityExistsAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity;
        Task<bool> EntityExistsAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity;
        EntityEntry Entry(object entity);

        IQueryable<SqlFunctionIdResult> GetAppointmentIdsForPerson(Guid personId);
        IQueryable<SqlFunctionIdResult> GetPersonsForAppointment(Guid appointmentId);
        IQueryable<SqlFunctionIdResult> GetMusicianProfilesForAppointment(Guid appointmentId);
        bool IsPersonEligibleForAppointment(Guid personId, Guid appointmentId);
        Task<int> ExecuteSqlAsync(string sqlStatement);
        bool EntityExists<TEntity>(Guid id) where TEntity : BaseEntity;

        /// <summary>
        /// Global search across Persons, Appointments, Projects, and News
        /// </summary>
        /// <param name="searchQuery">Search query (supports partial matches and fuzzy search)</param>
        /// <param name="maxResults">Maximum number of results to return (default: 50)</param>
        /// <returns>List of search results ordered by relevance</returns>
        Task<List<GlobalSearchResult>> GlobalSearchAsync(string searchQuery, int maxResults = 50);
    }
}

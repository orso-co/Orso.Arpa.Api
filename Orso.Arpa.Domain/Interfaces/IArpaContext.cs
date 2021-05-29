using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Views;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IArpaContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<AppointmentParticipation> AppointmentParticipations { get; set; }
        DbSet<AppointmentRoom> AppointmentRooms { get; set; }
        DbSet<MusicianProfile> MusicianProfiles { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<PersonAddress> PersonAddresses { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Url> Urls { get; set; }
        DbSet<UrlRole> UrlRoles { get; set; }
        DbSet<ProjectAppointment> ProjectAppointments { get; set; }
        DbSet<ProjectParticipation> ProjectParticipations { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<SectionAppointment> SectionAppointments { get; set; }
        DbSet<SelectValueCategory> SelectValueCategories { get; set; }
        DbSet<SelectValue> SelectValues { get; set; }
        DbSet<SelectValueMapping> SelectValueMappings { get; set; }
        DbSet<Venue> Venues { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void ClearChangeTracker();
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        EntityEntry Remove(object entity);

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> EntityExistsAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity;

        Task<bool> EntityExistsAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;

        EntityEntry Entry(object entity);
        IQueryable<AppointmentForPerson> GetAppointmentIdsForPerson(Guid personId);
    }
}

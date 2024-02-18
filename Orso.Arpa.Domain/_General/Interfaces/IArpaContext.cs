using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain._General.Interfaces;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IArpaContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void ClearChangeTracker();
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
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
        Task<TVersionedEntity> GetCurrentAsync<TVersionedEntity>(CancellationToken cancellationToken) where TVersionedEntity : BaseEntity, IVersionedEntity;
    }
}

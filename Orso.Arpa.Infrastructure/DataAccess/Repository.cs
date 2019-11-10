using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence;

namespace Orso.Arpa.Infrastructure.DataAccess
{
    public class Repository : IRepository
    {
        private readonly IUserAccessor _userAccessor;
        private readonly ArpaContext _arpaContext;

        public Repository(IUserAccessor userAccessor, ArpaContext arpaContext)
        {
            _userAccessor = userAccessor;
            _arpaContext = arpaContext;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.Create(_userAccessor.DisplayName);
            await _arpaContext.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _arpaContext.Dispose();
            }
        }

        public async Task DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            TEntity entity = await _arpaContext.Set<TEntity>().FindAsync(id);
            entity.Delete(_userAccessor.DisplayName);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.Modify(_userAccessor.DisplayName);
            _arpaContext.Set<TEntity>().Update(entity);
        }
    }
}

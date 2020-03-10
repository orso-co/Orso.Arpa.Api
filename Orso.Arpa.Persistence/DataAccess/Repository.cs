using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Persistence.DataAccess
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
            await _arpaContext.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return _arpaContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            TEntity entity = await _arpaContext.Set<TEntity>().FindAsync(id);
            NullCheck(entity);
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
            TEntity entity = await GetByIdAsync<TEntity>(id);
            NullCheck(entity);
            entity.Delete(_userAccessor.DisplayName);
        }

        private static void NullCheck<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                throw new RestException($"{entityName} not found", HttpStatusCode.NotFound, new
                {
                    Entity = "Not found"
                });
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _arpaContext.Set<TEntity>().Update(entity);
        }

        public bool Exists<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return Exists<TEntity>(e => e.Id == id);
        }

        public bool Exists<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return _arpaContext.Set<TEntity>().Any(predicate);
        }
    }
}

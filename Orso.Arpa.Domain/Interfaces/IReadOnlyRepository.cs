using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IReadOnlyRepository : IDisposable
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        bool Exists<TEntity>(Guid id) where TEntity : BaseEntity;

        bool Exists<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
    }
}

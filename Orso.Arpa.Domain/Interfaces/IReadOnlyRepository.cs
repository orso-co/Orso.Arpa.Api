using System;
using System.Linq;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IReadOnlyRepository : IDisposable
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity;
    }
}

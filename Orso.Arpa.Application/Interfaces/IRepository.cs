using System;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}

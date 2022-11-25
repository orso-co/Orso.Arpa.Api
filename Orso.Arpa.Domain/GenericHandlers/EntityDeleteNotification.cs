using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.GenericHandlers
{
    public interface IEntityDeleteNotification<TEntity> : INotification where TEntity : BaseEntity
    {
        Guid Id { get; set; }
    }
}

using System;
using MediatR;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.General.GenericHandlers
{
    public interface IEntityDeleteNotification<TEntity> : INotification where TEntity : BaseEntity
    {
        Guid Id { get; set; }
    }
}

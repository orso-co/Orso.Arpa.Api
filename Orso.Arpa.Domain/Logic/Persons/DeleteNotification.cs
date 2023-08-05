using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public class DeleteNotification : IEntityDeleteNotification<Person>
    {
        public Guid Id { get; set; }
    }

    public class ContactViaHandler : INotificationHandler<DeleteNotification>
    {
        private readonly IArpaContext _arpaContext;

        public ContactViaHandler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task Handle(DeleteNotification notification, CancellationToken cancellationToken)
        {
            IList<Person> personsWithContactVia = await _arpaContext.Set<Person>()
                .Where(a => notification.Id.Equals(a.ContactViaId))
                .ToListAsync(cancellationToken);

            foreach (Person person in personsWithContactVia)
            {
                person.ClearContactVia();
                _arpaContext.Entry(person)?.CurrentValues?.SetValues(person);
            }

            if ((await _arpaContext.SaveChangesAsync(cancellationToken)) < personsWithContactVia.Count)
            {
                throw new AffectedRowCountMismatchException(nameof(Person));
            }
        }
    }

    public class UserHandler : INotificationHandler<DeleteNotification>
    {
        private readonly IArpaContext _arpaContext;
        private readonly ArpaUserManager _arpaUserManager;

        public UserHandler(IArpaContext arpaContext, ArpaUserManager arpaUserManager)
        {
            _arpaContext = arpaContext;
            _arpaUserManager = arpaUserManager;
        }

        public async Task Handle(DeleteNotification notification, CancellationToken cancellationToken)
        {
            Person person = await _arpaContext.FindAsync<Person>(new object[] { notification.Id }, cancellationToken);
            if (person.User != null)
            {
                _ = await _arpaUserManager.DeleteAsync(person.User);
            }
        }
    }
}

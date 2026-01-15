using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public class PersonDeletedNotification : IEntityDeleteNotification<Person>
    {
        public Guid Id { get; set; }
    }

    public class ContactViaHandler : INotificationHandler<PersonDeletedNotification>
    {
        private readonly IArpaContext _arpaContext;

        public ContactViaHandler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task Handle(PersonDeletedNotification notification, CancellationToken cancellationToken)
        {
            List<Person> personsWithContactVia = await _arpaContext.Set<Person>()
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

    public class UserHandler : INotificationHandler<PersonDeletedNotification>
    {
        private readonly IArpaContext _arpaContext;
        private readonly ArpaUserManager _arpaUserManager;

        public UserHandler(IArpaContext arpaContext, ArpaUserManager arpaUserManager)
        {
            _arpaContext = arpaContext;
            _arpaUserManager = arpaUserManager;
        }

        public async Task Handle(PersonDeletedNotification notification, CancellationToken cancellationToken)
        {
            Person person = await _arpaContext.FindAsync<Person>(new object[] { notification.Id }, cancellationToken);
            if (person.User != null)
            {
                _ = await _arpaUserManager.DeleteAsync(person.User);
            }
        }
    }
}

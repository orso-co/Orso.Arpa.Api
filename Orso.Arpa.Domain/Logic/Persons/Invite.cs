using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public class PersonInviteResult
    {
        public IDictionary<string, string> SuccessfulInvites { get; set; } = new Dictionary<string, string>();

        public IList<string> PersonsWithoutEmailAddress { get; set; } = new List<string>();
        public IList<string> PersonsAlreadyRegistered { get; set; } = new List<string>();
        public IDictionary<string, IList<string>> PersonsWithMultipleEmailAddresses { get; set; } = new Dictionary<string, IList<string>>();
        public IDictionary<string, string> FailedInvites { get; set; } = new Dictionary<string, string>();
    }

    public static class Invite
    {
        public class Command : IRequest<PersonInviteResult>
        {
            public IList<Guid> PersonIds { get; set; } = new List<Guid>();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleForEach(cmd => cmd.PersonIds)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, PersonInviteResult>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IEmailSender _emailSender;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration;

            public Handler(
                IArpaContext arpaContext,
                IEmailSender emailSender,
                JwtConfiguration jwtConfiguration,
                ClubConfiguration clubConfiguration)
            {
                _arpaContext = arpaContext;
                _emailSender = emailSender;
                _jwtConfiguration = jwtConfiguration;
                _clubConfiguration = clubConfiguration;
            }

            public async Task<PersonInviteResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = new PersonInviteResult();
                var toInvite = new Dictionary<string, string>();

                List<Person> persons = await _arpaContext.Persons
                    .AsQueryable()
                    .Where(person => request.PersonIds.Contains(person.Id))
                    .ToListAsync();

                foreach (Person person in persons)
                {
                    if (person.User is not null)
                    {
                        result.PersonsAlreadyRegistered.Add(person.DisplayName);
                        continue;
                    }
                    var emailAdresses = person.ContactDetails.Where(cd => cd.Key == ContactDetailKey.EMail).Select(cd => cd.Value).ToList();
                    switch (emailAdresses.Count)
                    {
                        case 0:
                            result.PersonsWithoutEmailAddress.Add(person.DisplayName);
                            continue;
                        case 1:
                            toInvite.Add(person.DisplayName, emailAdresses[0]);
                            break;
                        default:
                            result.PersonsWithMultipleEmailAddresses.Add(person.DisplayName, emailAdresses);
                            toInvite.Add(person.DisplayName, emailAdresses[0]);
                            break;
                    }
                }

                foreach (KeyValuePair<string, string> item in toInvite)
                {
                    var template = new InvitePersonTemplate
                    {
                        DisplayName = item.Key,
                        ArpaLogo = $"{_jwtConfiguration.Audience}/images/arpa_logo.png",
                        ClubAddress = _clubConfiguration.Address,
                        ClubMail = _clubConfiguration.Email,
                        ClubName = _clubConfiguration.Name,
                        ClubPhoneNumber = _clubConfiguration.Phone
                    };

                    try
                    {
                        await _emailSender.SendTemplatedEmailAsync(template, item.Value);
                    }
                    catch (Exception ex)
                    {
                        result.FailedInvites.Add(item.Key, ex.Message);
                        continue;
                    }

                    result.SuccessfulInvites.Add(item.Key, item.Value);
                }

                return result;
            }
        }
    }
}

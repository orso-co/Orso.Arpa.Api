using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class UserRegister
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public Guid GenderId { get; set; }
            public string ClientUri { get; set; }

            public IList<Guid> StakeholderGroupIds { get; set; } = new List<Guid>();
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Person>()
                    .ForMember(p => p.GivenName, opt => opt.MapFrom(src => src.GivenName))
                    .ForMember(p => p.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(p => p.DateOfBirth, opt => opt.MapFrom((src, dest) => src.DateOfBirth > DateTime.MinValue ? src.DateOfBirth : dest.DateOfBirth))
                    .ForMember(p => p.GenderId, opt => opt.MapFrom(src => src.GenderId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager, IArpaContext context)
            {
                RuleFor(c => c.UserName)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) == null)
                    .WithMessage("Username aleady exists");
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) == null)
                    .WithMessage("Email aleady exists");
                RuleForEach(c => c.StakeholderGroupIds)
                    .EntityExists<Command, Section>(context);
                RuleFor(c => c.GenderId)
                    .SelectValueMapping<Command, Person>(context, p => p.Gender);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IDateTimeProvider _dateTimeProvider;
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(
                ArpaUserManager userManager,
                IDateTimeProvider dateTimeProvider,
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _userManager = userManager;
                _dateTimeProvider = dateTimeProvider;
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingPersons = _arpaContext.Persons
                    .AsQueryable()
                    .Where(p =>
                        p.User == null
                        && p.ContactDetails.Any(detail =>
                            detail.Key == ContactDetailKey.EMail
#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
                            && detail.Value.ToLower() == request.Email.ToLower()))
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
                    .ToList();

                Person person;

                switch (existingPersons.Count)
                {
                    case 0:
                        person = new Person(Guid.NewGuid(), request);
                        break;
                    case 1:
                        Person existingPerson = existingPersons[0];
                        if (!(existingPerson.GivenName.Equals(request.GivenName) && existingPerson.Surname.Equals(request.Surname)))
                        {
                            throw new AuthorizationException("You are not allowed to register with this e-mail address");
                        }
                        person = existingPerson;
                        _mapper.Map(request, person);
                        break;
                    default:
                        throw new ValidationException(
                            new ValidationFailure[] { new ValidationFailure(nameof(Command.Email), "Multiple persons found with this email address. Registration aborted. Please contact your system admin.") });
                }



                var user = new User
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    Person = person,
                    CreatedAt = _dateTimeProvider.GetUtcNow()
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new IdentityException("Problem creating user", result.Errors);
                }

                _arpaContext.Set<PersonSection>().AddRange(request.StakeholderGroupIds.Select(sg => new PersonSection(null, person.Id, sg)));

                if ((await _arpaContext.SaveChangesAsync(cancellationToken)) < request.StakeholderGroupIds.Count)
                {
                    throw new Exception("Problem creating stakeholder groups");
                }

                return Unit.Value;
            }
        }
    }
}
